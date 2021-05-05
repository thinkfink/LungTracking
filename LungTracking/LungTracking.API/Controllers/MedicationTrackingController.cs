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
    public class MedicationTrackingController : ControllerBase
    {
        // GET: api/<MedicationTrackingController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationTracking>>> Get()
        {
            // Return all the medicationTrackings
            try
            {
                return Ok(await MedicationTrackingManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<MedicationTrackingController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<MedicationTracking>> Get(Guid patientId)
        {
            try
            {
                return Ok(await MedicationTrackingManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<MedicationTrackingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicationTracking medicationTracking)
        {
            try
            {
                return Ok(await MedicationTrackingManager.Insert(medicationTracking));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<MedicationTrackingController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MedicationTracking medicationTracking)
        {
            try
            {
                return Ok(await MedicationTrackingManager.Update(medicationTracking));
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
                return Ok(await MedicationTrackingManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
