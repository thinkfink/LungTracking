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
    public class SleepWakeController : ControllerBase
    {
        // GET: api/<SleepWakeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SleepWake>>> Get()
        {
            // Return all the sleepWakes
            try
            {
                return Ok(await SleepWakeManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<SleepWakeController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<SleepWake>> Get(Guid patientId)
        {
            try
            {
                return Ok(await SleepWakeManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<SleepWakeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SleepWake sleepWake)
        {
            try
            {
                return Ok(await SleepWakeManager.Insert(sleepWake));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<SleepWakeController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SleepWake sleepWake)
        {
            try
            {
                return Ok(await SleepWakeManager.Update(sleepWake));
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
                return Ok(await SleepWakeManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
