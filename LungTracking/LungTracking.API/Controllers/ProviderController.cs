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
    public class ProviderController : ControllerBase
    {
        // GET: api/<ProviderController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> Get()
        {
            // Return all the providers
            try
            {
                return Ok(await ProviderManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<ProviderController>/5
        [HttpGet("{userId:Guid}")]
        public async Task<ActionResult<Provider>> Get(Guid userId)
        {
            try
            {
                return Ok(await ProviderManager.LoadByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<ProviderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Provider provider)
        {
            try
            {
                return Ok(await ProviderManager.Insert(provider));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<ProviderController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Provider provider)
        {
            try
            {
                return Ok(await ProviderManager.Update(provider));
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
                return Ok(await ProviderManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
