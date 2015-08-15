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
    [RoutePrefix("api/patients")]
    public class PatientController : ApiController
    {
        IConnectionService _connectionService;

        public PatientController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        /// <summary>
        /// Get Patients by Pedia Id
        /// </summary>
        /// <param name="pediaId"></param>
        /// <returns></returns>
        [Route("{pediaId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(Guid pediaId)
        {
            return Ok(await _connectionService.GetPatientsAsync(pediaId));
        }

        /// <summary>
        /// Create Patients with Pediatrician
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetWithPediaAsync(Guid childId)
        {
            return Ok(await _connectionService.GetPediatriciansAsync(childId));
        }
        
        /// <summary>
        /// Create Patient Entry
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="pediaId"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> PostPatient(Guid childId, Guid pediaId)
        {
            try
            {
                if (childId == Guid.Empty)
                    return BadRequest();

                if (pediaId == Guid.Empty)
                    return BadRequest();

                var result = await _connectionService.AddPatientAsync(childId, pediaId);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Delete Patient entry
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [Route("{patientId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid patientId)
        {
            if (patientId == Guid.Empty)
                return NotFound();

            await _connectionService.RemovePatientAsync(patientId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
