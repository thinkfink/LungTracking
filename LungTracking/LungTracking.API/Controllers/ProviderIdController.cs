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
    public class ProviderIdController : ControllerBase
    {

        // GET api/<ProviderController>/5
        [HttpGet("{providerId:Guid}")]
        public async Task<ActionResult<Provider>> Get(Guid providerId)
        {
            try
            {
                return Ok(await ProviderManager.LoadByProviderId(providerId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
