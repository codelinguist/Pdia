using Microsoft.Azure;
using Pdia.Entities;
using Pdia.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Pdia.WebApi.Filters
{
    public class RequireTokenAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            AppClaim claim = null;
            //Check to see if the header contains a token entry
            if (actionContext.Request.Headers.Contains("X-API-TOKENAUTH"))
            {

                string secretKey = CloudConfigurationManager.GetSetting("FaceOffers.API.SharedSecretKey");

                try
                {
                    //Decode the token
                    string token = actionContext.Request.Headers.GetValues("X-API-TOKENAUTH").First();
                    claim = TokenManager.DecodeClaim(token, secretKey);
                    claim.Token = token;

                }
                catch (TokenManager.SignatureVerificationException)
                {
                    ReturnError(HttpStatusCode.Forbidden, "This token is not valid, please refresh token or obtain a valid token!");
                }
                catch (Exception)
                {

                    ReturnError(HttpStatusCode.Forbidden, "Error decoding token, please try again.");
                }

                if (claim == null || claim.Expires < DateTime.UtcNow)
                {
                    //The token is invalid or already expired
                    ReturnError(HttpStatusCode.Forbidden, "This token is not valid, please refresh token or obtain a valid token!");
                }
                else
                {
                    //The client version of the token looks good, verify it against the serer
                    if (!(await TokenManager.ValidateClaim(claim)))
                    {
                        //Verifitcation failed
                        ReturnError(HttpStatusCode.Forbidden, "This token is not valid, please refresh token or obtain a valid token!");
                    }
                }
            }
            else
            {
                ReturnError(HttpStatusCode.Unauthorized, "No api and token found on header.");
            }
            actionContext.ActionArguments["Claim"] = claim;
            base.OnActionExecuting(actionContext);
        }

        private void ReturnError(HttpStatusCode statusCode, string message)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(message),
                StatusCode = statusCode
            };

            throw new HttpResponseException(response);
        }
    }
}