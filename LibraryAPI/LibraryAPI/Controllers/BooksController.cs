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
    public class BooksController : ControllerBase
    {
        private readonly IService<Books> _bookService;
        public BooksController(IService<Books> bookService)
        {
            _bookService = bookService;
        }
        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Books>> Get()
        {
            try
            {
                var result = _bookService.GetAll().ToList();
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<IEnumerable<Books>> GetById(int id)
        {
            try
            {
                var result = _bookService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<IEnumerable<Books>> Post([FromBody] Books book)
        {
            try
            {
                return Ok(_bookService.Add(book));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Books>> Put(int id, [FromBody] Books book)
        {
            try
            {
                return Ok(_bookService.Update(book));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Books>> Delete(int id)
        {
            try
            {
                return Ok(_bookService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
