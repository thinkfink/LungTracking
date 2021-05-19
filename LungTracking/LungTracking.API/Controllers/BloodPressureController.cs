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
    public class BloodPressureController : ControllerBase
    {
        // GET: api/<BloodPressureController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodPressure>>> Get()
        {
            // Return all the bloodPressures
            try
            {
                return Ok(await BloodPressureManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<BloodPressureController>/5
        [HttpGet("{patientId:Guid}")]
        public async Task<ActionResult<BloodPressure>> Get(Guid patientId)
        {
            try
            {
                return Ok(await BloodPressureManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<BloodPressureController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BloodPressure bloodPressure)
        {
            try
            {
                return Ok(await BloodPressureManager.Insert(bloodPressure));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<BloodPressureController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BloodPressure bloodPressure)
        {
            try
            {
                return Ok(await BloodPressureManager.Update(bloodPressure));
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
                return Ok(await BloodPressureManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
