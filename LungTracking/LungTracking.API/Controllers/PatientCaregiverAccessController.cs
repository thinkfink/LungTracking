using LungTracking.BL;
using LungTracking.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LungTracking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientCaregiverAccessController : ControllerBase
    {
        // GET: api/<PatientCaregiverAccessController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCaregiverAccess>>> Get()
        {
            // Return all the patientCaregiverAccesss
            try
            {
                return Ok(await PatientCaregiverAccessManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/<PatientCaregiverAccessController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCaregiverAccess>>> GetById(Guid patientId, Guid caregiverId)
        {
            // Return all the patientCaregiverAccesss
            try
            {
                return Ok(await PatientCaregiverAccessManager.LoadById(patientId, caregiverId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PatientCaregiverAccessController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientCaregiverAccess patientCaregiverAccess)
        {
            try
            {
                return Ok(await PatientCaregiverAccessManager.Insert(patientCaregiverAccess));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<ActivationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await PatientCaregiverAccessManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
