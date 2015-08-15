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
    [RoutePrefix("api/parenting")]
    public class ParentingController : ApiController
    {
        IParentingService _parentingService;

        public ParentingController(IParentingService parentingService)
        {
            _parentingService = parentingService;
        }


        /// <summary>
        /// Get Parenting Information by Id
        /// </summary>
        /// <param name="parentingId"></param>
        /// <returns></returns>
        [Route("{parentingId}")]
        [HttpGet]
        [ResponseType(typeof(Parenting))]
        public async Task<IHttpActionResult> GetById(Guid parentingId)
        {
            return Ok(await _parentingService.FindAsync(parentingId));
        }

        /// <summary>
        /// Create Parenting Entry
        /// </summary>
        /// <param name="parenting"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Parenting))]
        public async Task<IHttpActionResult> PostParenting(Parenting parenting)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _parentingService.InsertAsync(parenting);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Parenting Entry
        /// </summary>
        /// <param name="parenting"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Parenting))]
        public async Task<IHttpActionResult> PutParenting(Parenting parenting)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (parenting.Id == Guid.Empty)
                    return BadRequest();

                var result = await _parentingService.UpdateAsync(parenting);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Parenting Entry
        /// </summary>
        /// <param name="parentingId"></param>
        /// <returns></returns>
        [Route("{parentingId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid parentingId)
        {
            if (parentingId == Guid.Empty)
                return NotFound();

            Parenting parenting = await _parentingService.FindAsync(parentingId);
            if (parenting == null)
                return NotFound();

            await _parentingService.DeleteAsync(parenting.Id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
