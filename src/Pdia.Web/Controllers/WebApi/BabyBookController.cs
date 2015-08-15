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
        [Route("{babyBookId}")]
        [HttpGet]
        [ResponseType(typeof(BabyBook))]
        public async Task<IHttpActionResult> GetById(Guid babyBookId)
        {
            return Ok(await _babyBookService.FindAsync(babyBookId));
        }

        /// <summary>
        /// Create BabyBook
        /// </summary>
        /// <param name="babyBook"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BabyBook))]
        public async Task<IHttpActionResult> PostBabyBook(BabyBook babyBook)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _babyBookService.InsertAsync(babyBook);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update BabyBook Info
        /// </summary>
        /// <param name="babyBook"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Pediatrician))]
        public async Task<IHttpActionResult> PutBabyBook(BabyBook babyBook)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (babyBook.Id == Guid.Empty)
                    return BadRequest();

                var result = await _babyBookService.UpdateAsync(babyBook);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag BabyBook as deleted
        /// </summary>
        /// <param name="babyBookId"></param>
        /// <returns></returns>
        [Route("{babyBookId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid babyBookId)
        {
            if (babyBookId == Guid.Empty)
                return NotFound();

            BabyBook babyBook = await _babyBookService.FindAsync(babyBookId);
            if (babyBook == null)
                return NotFound();

            await _babyBookService.DeleteAsync(babyBook);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
