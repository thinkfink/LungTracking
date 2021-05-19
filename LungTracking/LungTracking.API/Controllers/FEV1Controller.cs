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
    public class FEV1Controller : ControllerBase
    {
        // GET: api/<FEV1Controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FEV1>>> Get()
        {
            // Return all the fev1s
            try
            {
                return Ok(await FEV1Manager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<FEV1Controller>/5
        [HttpGet("{patientId:Guid}")]
        public async Task<ActionResult<FEV1>> Get(Guid patientId)
        {
            try
            {
                return Ok(await FEV1Manager.LoadByPatientId(patientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<FEV1Controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FEV1 fev1)
        {
            try
            {
                return Ok(await FEV1Manager.Insert(fev1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<FEV1Controller>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FEV1 fev1)
        {
            try
            {
                return Ok(await FEV1Manager.Update(fev1));
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
                return Ok(await FEV1Manager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
