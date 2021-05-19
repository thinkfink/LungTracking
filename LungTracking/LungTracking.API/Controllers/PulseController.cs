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
    public class PulseController : ControllerBase
    {
        // GET: api/<PulseController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pulse>>> Get()
        {
            // Return all the pulses
            try
            {
                return Ok(await PulseManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<PulseController>/5
        [HttpGet("{patientId:Guid}")]
        public async Task<ActionResult<Pulse>> Get(Guid patientId)
        {
            try
            {
                return Ok(await PulseManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PulseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pulse pulse)
        {
            try
            {
                return Ok(await PulseManager.Insert(pulse));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<PulseController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Pulse pulse)
        {
            try
            {
                return Ok(await PulseManager.Update(pulse));
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
                return Ok(await PulseManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
