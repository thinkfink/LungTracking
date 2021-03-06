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
    public class MedicationTimeController : ControllerBase
    {
        // GET: api/<MedicationTimeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationTime>>> Get()
        {
            // Return all the medicationTimes
            try
            {
                return Ok(await MedicationTimeManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<MedicationTimeController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<MedicationTime>> Get(Guid patientId)
        {
            try
            {
                return Ok(await MedicationTimeManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<MedicationTimeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicationTime medicationTime)
        {
            try
            {
                return Ok(await MedicationTimeManager.Insert(medicationTime));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<MedicationTimeController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MedicationTime medicationTime)
        {
            try
            {
                return Ok(await MedicationTimeManager.Update(medicationTime));
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
                return Ok(await MedicationTimeManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
