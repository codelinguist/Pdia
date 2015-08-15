using Pdia.Entities;
using Pdia.Infrastructure;
using Pdia.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Pdia.Web.Controllers.WebApi
{
    [RequireToken]
    [RoutePrefix("api/useraccount")]
    public class UserAccountController : ApiController
    {
        IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        /// <summary>
        /// Get UserAccount Information by Id
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns></returns>
        [Route("{userAccountId}")]
        [HttpGet]
        [ResponseType(typeof(UserAccount))]
        public async Task<IHttpActionResult> GetById(string userAccountId)
        {
            return Ok(await _userAccountService.FindAsync(userAccountId));
        }

        /// <summary>
        /// Create UserAccount
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(UserAccount))]
        public async Task<IHttpActionResult> PostUserAccount(UserAccount userAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _userAccountService.InsertAsync(userAccount);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update UserAccount Info
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Pediatrician))]
        public async Task<IHttpActionResult> PutUserAccount(UserAccount userAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (String.IsNullOrEmpty(userAccount.Id))
                    return BadRequest();

                var result = await _userAccountService.UpdateAsync(userAccount);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag UserAccount as deleted
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns></returns>
        [Route("{userAccountId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string userAccountId)
        {
            if (String.IsNullOrEmpty(userAccountId))
                return NotFound();

            UserAccount userAccount = await _userAccountService.FindAsync(userAccountId);
            if (userAccount == null)
                return NotFound();

            await _userAccountService.DeleteAsync(userAccountId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
