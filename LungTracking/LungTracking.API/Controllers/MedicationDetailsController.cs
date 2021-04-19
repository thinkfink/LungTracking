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
    public class MedicationDetailsController : ControllerBase
    {
        // GET: api/<MedicationDetailsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationDetails>>> Get()
        {
            // Return all the medicationDetailss
            try
            {
                return Ok(await MedicationDetailsManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<MedicationDetailsController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<MedicationDetails>> GetByPatientId(Guid patientId)
        {
            try
            {
                return Ok(await MedicationDetailsManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<MedicationDetailsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicationDetails medicationDetails)
        {
            try
            {
                return Ok(await MedicationDetailsManager.Insert(medicationDetails));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<MedicationDetailsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MedicationDetails medicationDetails)
        {
            try
            {
                return Ok(await MedicationDetailsManager.Update(medicationDetails));
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
                return Ok(await MedicationDetailsManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
