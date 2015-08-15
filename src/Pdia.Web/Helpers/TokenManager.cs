using Ninject;
using Pdia.Entities;
using Pdia.Infrastructure;
using Pdia.Services;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Pdia.Web.Helpers
{
    public class TokenManager
    {
        //claims cache
        private static Dictionary<Guid, AppClaim> _claims;
        private static DateTime _memoryFlush;
        private static IAuthorizationService _authService;
        static TokenManager()
        {
            _authService = WebModule.Kernel.Get<IAuthorizationService>();
            _memoryFlush = DateTime.UtcNow;
            _claims = new Dictionary<Guid, AppClaim>();
        }

        public static async Task<bool> ValidateClaim(AppClaim claim)
        {
            //Get the claim from the database
            AppClaim dbClaim = await GetClaim(claim.Id);

            //Verify the claim and return the resultsB
            return VerifyClaim(dbClaim, claim);
        }

        private static async Task<AppClaim> GetClaim(Guid claimID)
        {
            //Get the claim from memory
            AppClaim existing = GetClaimFromMemory(claimID);

            if (existing == null)
            {
                //No claim in memory, check the database
                existing = await _authService.GetClaimAsync(claimID);

                if (existing != null) { _claims[existing.Id] = existing; }

                return existing;
            }
            else
            {
                //Found the claim in memory, return it
                return existing;
            }
        }

        private static AppClaim GetClaimFromMemory(Guid claimID)
        {
            if (_memoryFlush > DateTime.UtcNow)
            {
                AppClaim claim;
                _claims.TryGetValue(claimID, out claim);
                return claim;
            }
            else
            {
                lock (_claims)
                {
                    _claims = new Dictionary<Guid, AppClaim>();
                    _memoryFlush = DateTime.UtcNow.AddMinutes(30);
                }

                return null;
            }
        }

        private static bool VerifyClaim(AppClaim server, AppClaim client)
        {
            if (server == null) { return false; }
            if (client == null) { return false; }
            if (server.Revoked == true) { return false; }
            if (server.Token != client.Token) { return false; }
            //if (server.UserName != client.UserName) { return false; }
            if (server.Expires < DateTime.UtcNow) { return false; }

            client.Token = server.Token;
            return true;
        }

        public static async Task<AppClaim> GenerateClaim(string secretKey, int expiresMin)
        {
            AppClaim claim = new AppClaim() { Expires = DateTime.UtcNow.AddMinutes(expiresMin), Id = Guid.NewGuid(), Revoked = false };

            claim.Token = JsonWebToken.Encode(claim, secretKey, JwtHashAlgorithm.HS256);
            await _authService.InsertClaimAsync(claim);

            return claim;
        }

        public static AppClaim DecodeClaim(string token, string secretKey)
        {
            string jsonPayload = JsonWebToken.Decode(token, secretKey);
            Console.Out.WriteLine(jsonPayload);

            AppClaim claim = JsonWebToken.Decode<AppClaim>(token, secretKey);

            return claim;
        }

        private enum JwtHashAlgorithm
        {
            HS256,
            HS384,
            HS512
        }

        /// <summary>
        /// Provides methods for encoding and decoding JSON Web Tokens.
        /// </summary>
        private static class JsonWebToken
        {
            private static Dictionary<JwtHashAlgorithm, Func<byte[], byte[], byte[]>> HashAlgorithms;
            //private static JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            static JsonWebToken()
            {
                HashAlgorithms = new Dictionary<JwtHashAlgorithm, Func<byte[], byte[], byte[]>>
            {
                { JwtHashAlgorithm.HS256, (key, value) => { using (var sha = new HMACSHA256(key)) { return sha.ComputeHash(value); } } },
                { JwtHashAlgorithm.HS384, (key, value) => { using (var sha = new HMACSHA384(key)) { return sha.ComputeHash(value); } } },
                { JwtHashAlgorithm.HS512, (key, value) => { using (var sha = new HMACSHA512(key)) { return sha.ComputeHash(value); } } }
            };
            }

            /// <summary>
            /// Creates a JWT given a payload, the signing key, and the algorithm to use.
            /// </summary>
            /// <param name="payload">An arbitrary payload (must be serializable to JSON via <see cref="System.Web.Script.Serialization.JavaScriptSerializer"/>).</param>
            /// <param name="key">The key used to sign the token.</param>
            /// <param name="algorithm">The hash algorithm to use.</param>
            /// <returns>The generated JWT.</returns>
            public static string Encode(object payload, string key, JwtHashAlgorithm algorithm)
            {
                var segments = new List<string>();
                var header = new { typ = "JWT", alg = algorithm.ToString() };

                byte[] headerBytes = Encoding.UTF8.GetBytes(ServiceStack.Text.JsonSerializer.SerializeToString(header));
                byte[] payloadBytes = Encoding.UTF8.GetBytes(ServiceStack.Text.JsonSerializer.SerializeToString(payload));


                //byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.ToString(header));
                //byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.ToString(payload));

                segments.Add(Base64UrlEncode(headerBytes));
                segments.Add(Base64UrlEncode(payloadBytes));

                var stringToSign = string.Join(".", segments.ToArray());

                var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);
                var keyBytes = Encoding.UTF8.GetBytes(key);

                byte[] signature = HashAlgorithms[algorithm](keyBytes, bytesToSign);
                segments.Add(Base64UrlEncode(signature));

                return string.Join(".", segments.ToArray());
            }

            /// <summary>
            /// Given a JWT, decode it and return the JSON payload.
            /// </summary>
            /// <param name="token">The JWT.</param>
            /// <param name="key">The key that was used to sign the JWT.</param>
            /// <param name="verify">Whether to verify the signature (default is true).</param>
            /// <returns>A string containing the JSON payload.</returns>
            /// <exception cref="SignatureVerificationException">Thrown if the verify parameter was true and the signature was NOT valid or if the JWT was signed with an unsupported algorithm.</exception>
            public static string Decode(string token, string key, bool verify = true)
            {
                var parts = token.Split('.');
                var header = parts[0];
                var payload = parts[1];
                byte[] crypto = Base64UrlDecode(parts[2]);

                var headerJson = Encoding.UTF8.GetString(Base64UrlDecode(header));
                //var headerData = JsonConvert.DeserializeObject<Dictionary<string, object>>(headerJson);
                var headerData = JsonSerializer.DeserializeFromString<Dictionary<string, object>>(headerJson);
                var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));

                if (verify)
                {
                    var bytesToSign = Encoding.UTF8.GetBytes(string.Concat(header, ".", payload));
                    var keyBytes = Encoding.UTF8.GetBytes(key);
                    var algorithm = (string)headerData["alg"];

                    var signature = HashAlgorithms[GetHashAlgorithm(algorithm)](keyBytes, bytesToSign);
                    var decodedCrypto = Convert.ToBase64String(crypto);
                    var decodedSignature = Convert.ToBase64String(signature);

                    if (decodedCrypto != decodedSignature)
                    {
                        throw new SignatureVerificationException(string.Format("Invalid signature. Expected {0} got {1}", decodedCrypto, decodedSignature));
                    }
                }

                return payloadJson;
            }

            /// <summary>
            /// Given a JWT, decode it and return the payload as an object (by deserializing it with <see cref="System.Web.Script.Serialization.JavaScriptSerializer"/>).
            /// </summary>
            /// <param name="token">The JWT.</param>
            /// <param name="key">The key that was used to sign the JWT.</param>
            /// <param name="verify">Whether to verify the signature (default is true).</param>
            /// <returns>An object representing the payload.</returns>
            /// <exception cref="SignatureVerificationException">Thrown if the verify parameter was true and the signature was NOT valid or if the JWT was signed with an unsupported algorithm.</exception>
            public static T Decode<T>(string token, string key, bool verify = true)
            {
                var payloadJson = JsonWebToken.Decode(token, key, verify);
                //var payloadData = JsonConvert.DeserializeObject<T>(payloadJson);
                var payloadData = JsonSerializer.DeserializeFromString<T>(payloadJson);
                return payloadData;
            }

            private static JwtHashAlgorithm GetHashAlgorithm(string algorithm)
            {
                switch (algorithm)
                {
                    case "HS256": return JwtHashAlgorithm.HS256;
                    case "HS384": return JwtHashAlgorithm.HS384;
                    case "HS512": return JwtHashAlgorithm.HS512;
                    default: throw new SignatureVerificationException("Algorithm not supported.");
                }
            }

            // from JWT spec
            private static string Base64UrlEncode(byte[] input)
            {
                var output = Convert.ToBase64String(input);
                output = output.Split('=')[0]; // Remove any trailing '='s
                output = output.Replace('+', '-'); // 62nd char of encoding
                output = output.Replace('/', '_'); // 63rd char of encoding
                return output;
            }

            // from JWT spec
            private static byte[] Base64UrlDecode(string input)
            {
                var output = input;
                output = output.Replace('-', '+'); // 62nd char of encoding
                output = output.Replace('_', '/'); // 63rd char of encoding
                switch (output.Length % 4) // Pad with trailing '='s
                {
                    case 0: break; // No pad chars in this case
                    case 2: output += "=="; break; // Two pad chars
                    case 3: output += "="; break; // One pad char
                    default: throw new System.Exception("Illegal base64url string!");
                }
                var converted = Convert.FromBase64String(output); // Standard base64 decoder
                return converted;
            }
        }

        public class SignatureVerificationException : Exception
        {
            public SignatureVerificationException(string message)
                : base(message)
            {
            }
        }

    }
}