using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Core.ApplicationServices;
using LibraryApp.Core.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IService<Users> _userService;
        public UsersController(IService<Users> userservice)
        {
            _userService = userservice;
        }

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Users>> Get()
        {
            try
            {
                var result = _userService.GetAll().ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Books/5
        [HttpGet("{username}")]
        public ActionResult<IEnumerable<Users>> GetUserById(string username)
        {
            try
            {
                var result = _userService.GetByName(username);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<IEnumerable<Users>> Post([FromBody] Users user)
        {
            try
            {
                return Ok(_userService.Add(user));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Users>> Put(int id, [FromBody] Users user)
        {
            try
            {
                return Ok(_userService.Update(user));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Users>> Delete(int id)
        {
            try
            {
                return Ok(_userService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
