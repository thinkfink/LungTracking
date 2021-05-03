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
    public class PatientController : ControllerBase
    {
        // GET: api/<PatientController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get()
        {
            // Return all the patients
            try
            {
                return Ok(await PatientManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<PatientController>/5
        [HttpGet("{patientId:Guid}")]
        public async Task<ActionResult<Patient>> GetByPatientId(Guid patientId)
        {
            try
            {
                return Ok(await PatientManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<PatientController>/5
        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<Patient>> GetByUserId(Guid userId)
        {
            try
            {
                return Ok(await PatientManager.LoadByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PatientController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            try
            {
                return Ok(await PatientManager.Insert(patient));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<PatientController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Patient patient)
        {
            try
            {
                return Ok(await PatientManager.Update(patient));
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
                return Ok(await PatientManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
