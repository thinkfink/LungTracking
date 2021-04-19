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
    public class CaregiverController : ControllerBase
    {
        // GET: api/<CaregiverController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caregiver>>> Get()
        {
            // Return all the caregivers
            try
            {
                return Ok(await CaregiverManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<CaregiverController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Caregiver>> GetByCaregiverId(Guid caregiverId)
        {
            try
            {
                return Ok(await CaregiverManager.LoadByCaregiverId(caregiverId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<CaregiverController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Caregiver>> GetByUserId(Guid userId)
        {
            try
            {
                return Ok(await CaregiverManager.LoadByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<CaregiverController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Caregiver caregiver)
        {
            try
            {
                return Ok(await CaregiverManager.Insert(caregiver));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<CaregiverController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Caregiver caregiver)
        {
            try
            {
                return Ok(await CaregiverManager.Update(caregiver));
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
                return Ok(await CaregiverManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
