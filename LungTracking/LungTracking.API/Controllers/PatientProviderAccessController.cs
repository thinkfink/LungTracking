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
    public class PatientProviderAccessController : ControllerBase
    {
        // GET: api/<PatientProviderAccessController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientProviderAccess>>> Get()
        {
            // Return all the patientProviderAccesss
            try
            {
                return Ok(await PatientProviderAccessManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PatientProviderAccessController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientProviderAccess patientProviderAccess)
        {
            try
            {
                return Ok(await PatientProviderAccessManager.Insert(patientProviderAccess));
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
                return Ok(await PatientProviderAccessManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
