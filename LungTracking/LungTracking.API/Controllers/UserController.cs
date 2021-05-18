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
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            // Return all the users
            try
            {
                return Ok(await UserManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> Get(Guid userId)
        {
            try
            {
                return Ok(await UserManager.LoadByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{username:string}")]
        public async Task<ActionResult<User>> Get(string username)
        {
            try
            {
                return Ok(await UserManager.LoadByUsername(username));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                return Ok(await UserManager.Insert(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                return Ok(await UserManager.Update(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Update password
        // PUT api/<UserController>/5
        [HttpGet("updatepassword")]
        public async Task<IActionResult> Put([FromBody] Guid id, string password, string newPassword, string confirmPassword)
        {
            try
            {
                return Ok(await UserManager.UpdatePassword(id, password, newPassword, confirmPassword));
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
                return Ok(await UserManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
