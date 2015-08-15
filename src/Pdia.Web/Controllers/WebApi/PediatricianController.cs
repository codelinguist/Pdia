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
    [RoutePrefix("api/pediatricians")]
    public class PediatricianController : ApiController
    {
        IPediatricianService _pediatricianService;

        public PediatricianController(IPediatricianService pediatricianService)
        {
            _pediatricianService = pediatricianService;
        }

        /// <summary>
        /// Get Pediatrician Information by Id
        /// </summary>
        /// <param name="pediatricianId"></param>
        /// <returns></returns>
        [Route("{pediatricianId}")]
        [HttpGet]
        [ResponseType(typeof(Pediatrician))]
        public async Task<IHttpActionResult> GetById(Guid pediatricianId)
        {
            return Ok(await _pediatricianService.FindAsync(pediatricianId));
        }

        /// <summary>
        /// Create Pediatrician Account
        /// </summary>
        /// <param name="pediatrician"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Pediatrician))]
        public async Task<IHttpActionResult> PostPediatrician(Pediatrician pediatrician)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _pediatricianService.InsertAsync(pediatrician);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Pediatrician Info
        /// </summary>
        /// <param name="pediatrician"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Pediatrician))]
        public async Task<IHttpActionResult> PutPediatrician(Pediatrician pediatrician)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (pediatrician.Id == Guid.Empty)
                    return BadRequest();

                var result = await _pediatricianService.UpdateAsync(pediatrician);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag Pediatrician Account as deleted
        /// </summary>
        /// <param name="pediatricianId"></param>
        /// <returns></returns>
        [Route("{pediatricianId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid pediatricianId)
        {
            if (pediatricianId == Guid.Empty)
                return NotFound();

            Pediatrician pedia = await _pediatricianService.FindAsync(pediatricianId);
            if (pedia == null)
                return NotFound();

            await _pediatricianService.DeleteAsync(pedia);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
