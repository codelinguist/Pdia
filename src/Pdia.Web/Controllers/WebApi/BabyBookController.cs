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
    [RoutePrefix("api/babybook")]
    public class BabyBookController : ApiController
    {
        IBabyBookService _babyBookService;

        public BabyBookController(IBabyBookService babyBookService)
        {
            _babyBookService = babyBookService;
        }

        /// <summary>
        /// Get BabyBook Information by Id
        /// </summary>
        /// <param name="babyBookId"></param>
        /// <returns></returns>
        [Route("{babybookId}")]
        [HttpGet]
        [ResponseType(typeof(BabyBook))]
        public async Task<IHttpActionResult> GetById(Guid babyBookId)
        {
            return Ok(await _babyBookService.FindAsync(babyBookId));
        }
    }
}
