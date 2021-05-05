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
    public class PEFController : ControllerBase
    {
        // GET: api/<PEFController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PEF>>> Get()
        {
            // Return all the pefs
            try
            {
                return Ok(await PEFManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<PEFController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<PEF>> Get(Guid patientId)
        {
            try
            {
                return Ok(await PEFManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<PEFController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PEF pef)
        {
            try
            {
                return Ok(await PEFManager.Insert(pef));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<PEFController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PEF pef)
        {
            try
            {
                return Ok(await PEFManager.Update(pef));
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
                return Ok(await PEFManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
