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
    public class AppointmentIdController : ControllerBase
    {

        [HttpGet("{appointmentId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get(Guid appointmentId)
        {
            // Return all the appointments by appointmentId
            try
            {
                return Ok(await AppointmentManager.LoadByAppointmentId(appointmentId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
