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
    [RoutePrefix("api/child")]
    public class ChildController : ApiController
    {
        IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        /// <summary>
        /// Get Child Information by Id
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [Route("{childId}")]
        [HttpGet]
        [ResponseType(typeof(Child))]
        public async Task<IHttpActionResult> GetById(Guid childId)
        {
            return Ok(await _childService.FindAsync(childId));
        }

        /// <summary>
        /// Create Child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Child))]
        public async Task<IHttpActionResult> PostChild(Child child)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _childService.InsertAsync(child);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Child Info
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Child))]
        public async Task<IHttpActionResult> PutChild(Child child)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (child.Id == Guid.Empty)
                    return BadRequest();

                var result = await _childService.UpdateAsync(child);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag Child as deleted
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [Route("{childId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid childId)
        {
            if (childId == Guid.Empty)
                return NotFound();

            Child child = await _childService.FindAsync(childId);
            if (child == null)
                return NotFound();

            await _childService.DeleteAsync(child);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
