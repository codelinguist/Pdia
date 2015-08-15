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
    [RoutePrefix("api/clinic")]
    public class ClinicController : ApiController
    {
        IClinicService _clinicService;

        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        /// <summary>
        /// Get Clinic Information by Id
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [Route("{clinicId}")]
        [HttpGet]
        [ResponseType(typeof(Clinic))]
        public async Task<IHttpActionResult> GetById(Guid clinicId)
        {
            return Ok(await _clinicService.FindAsync(clinicId));
        }

        /// <summary>
        /// Create Clinic Info
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Clinic))]
        public async Task<IHttpActionResult> PostClinic(Clinic clinic)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _clinicService.InsertAsync(clinic);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Clinic Info
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Clinic))]
        public async Task<IHttpActionResult> PutClinic(Clinic clinic)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (clinic.Id == Guid.Empty)
                    return BadRequest();

                var result = await _clinicService.UpdateAsync(clinic);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag Clinic Account as deleted
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [Route("{clinicId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid clinicId)
        {
            if (clinicId == Guid.Empty)
                return NotFound();

            Clinic clinic = await _clinicService.FindAsync(clinicId);
            if (clinic == null)
                return NotFound();

            await _clinicService.DeleteAsync(clinic);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
