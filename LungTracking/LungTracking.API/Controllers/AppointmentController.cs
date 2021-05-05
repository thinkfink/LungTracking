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
    public class AppointmentController : ControllerBase
    {
        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            // Return all the appointments
            try
            {
                return Ok(await AppointmentManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Appointment>> Get(Guid patientId)
        {
            try
            {
                return Ok(await AppointmentManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Appointment appointment)
        {
            try
            {
                return Ok(await AppointmentManager.Insert(appointment));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Appointment appointment)
        {
            try
            {
                return Ok(await AppointmentManager.Update(appointment));
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
                return Ok(await AppointmentManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
