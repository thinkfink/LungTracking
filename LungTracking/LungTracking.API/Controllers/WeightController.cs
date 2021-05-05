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
    public class WeightController : ControllerBase
    {
        // GET: api/<WeightController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weight>>> Get()
        {
            // Return all the weights
            try
            {
                return Ok(await WeightManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<WeightController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Weight>> Get(Guid patientId)
        {
            try
            {
                return Ok(await WeightManager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<WeightController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Weight weight)
        {
            try
            {
                return Ok(await WeightManager.Insert(weight));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<WeightController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Weight weight)
        {
            try
            {
                return Ok(await WeightManager.Update(weight));
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
                return Ok(await WeightManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
