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
    public class TemperatureController : ControllerBase
    {
        // GET: api/<TemperatureController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temperature>>> Get()
        {
            // Return all the temperatures
            try
            {
                return Ok(await TemperatureManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<TemperatureController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Temperature>> GetByPatientId(Guid patientId)
        {
            try
            {
                return Ok(await TemperatureManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<TemperatureController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Temperature temperature)
        {
            try
            {
                return Ok(await TemperatureManager.Insert(temperature));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<TemperatureController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Temperature temperature)
        {
            try
            {
                return Ok(await TemperatureManager.Update(temperature));
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
                return Ok(await TemperatureManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
