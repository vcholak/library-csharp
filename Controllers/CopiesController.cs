using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopiesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public CopiesController(LibraryContext context)
        {
            _context = context;
        }

        // HEAD api/Copies
        [HttpHead]
        public async Task<ActionResult> Count()
        {
          int count = await _context.BookInstances.CountAsync();
          Response.Headers.Add("X-Result-Count", count.ToString());

          return NoContent();
        }

        // HEAD api/Copies/available
        [HttpHead("available")]
        public async Task<ActionResult> AvailableCount()
        {
          int count = await _context.BookInstances
          //.AllAsync()
          .CountAsync(); //TODO implement
          Response.Headers.Add("X-Result-Count", count.ToString());

          return NoContent();
        }

        // GET: api/Copies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookInstance>>> GetBookInstance()
        {
            return await _context.BookInstances.ToListAsync();
        }

        // GET: api/Copies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookInstance>> GetBookInstance(long id)
        {
            var bookInstance = await _context.BookInstances.FindAsync(id);

            if (bookInstance == null)
            {
                return NotFound();
            }

            return bookInstance;
        }

        // PUT: api/Copies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookInstance(long id, BookInstance bookInstance)
        {
            if (id != bookInstance.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookInstance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookInstanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Copies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookInstance>> PostBookInstance(BookInstance bookInstance)
        {
            _context.BookInstances.Add(bookInstance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookInstance), new { id = bookInstance.Id }, bookInstance);
        }

        // DELETE: api/Copies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookInstance(long id)
        {
            var bookInstance = await _context.BookInstances.FindAsync(id);
            if (bookInstance == null)
            {
                return NotFound();
            }

            _context.BookInstances.Remove(bookInstance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookInstanceExists(long id)
        {
            return _context.BookInstances.Any(e => e.Id == id);
        }
    }
}
