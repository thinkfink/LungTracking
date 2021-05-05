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
    public class BloodSugarController : ControllerBase
    {
        // GET: api/<BloodSugarController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodSugar>>> Get()
        {
            // Return all the bloodSugars
            try
            {
                return Ok(await BloodSugarManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<BloodSugarController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<BloodSugar>> Get(Guid patientId)
        {
            try
            {
                return Ok(await BloodSugarManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<BloodSugarController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BloodSugar bloodSugar)
        {
            try
            {
                return Ok(await BloodSugarManager.Insert(bloodSugar));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<BloodSugarController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BloodSugar bloodSugar)
        {
            try
            {
                return Ok(await BloodSugarManager.Update(bloodSugar));
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
                return Ok(await BloodSugarManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
