using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Library.Models;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopiesController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly ILogger<CopiesController> _logger;

        public CopiesController(LibraryContext context, ILogger<CopiesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // HEAD api/copies
        [HttpHead]
        public async Task<ActionResult> Count()
        {
          int count = await _context.BookInstances.CountAsync();
          Response.Headers.Add("X-Result-Count", count.ToString());

          return NoContent();
        }

        // HEAD api/copies/available
        [HttpHead("available")]
        public async Task<ActionResult> AvailableCount()
        {
          int count = await _context.BookInstances
          //.AllAsync()
          .CountAsync(); //TODO implement
          Response.Headers.Add("X-Result-Count", count.ToString());

          return NoContent();
        }

        // GET: api/copies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookInstance>>> AllBookInstances()
        {
            return await _context.BookInstances.ToListAsync();
        }

        // GET: api/copies/statuses
        [HttpGet("statuses")]
        public ActionResult<IEnumerable<string>> AllStatuses()
        {
            BookInstanceStatus[] arr = Enum.GetValues<BookInstanceStatus>();
            List<string> lst = new List<string>();
            foreach(BookInstanceStatus e in arr) {
              lst.Add(e.ToString());
            }
            return Ok(lst);
        }

        // GET: api/copies/5
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

        // PUT: api/copies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookInstance(long id, BookInstance bookInstance)
        {
            if (id != bookInstance.ID)
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

        // POST: api/copies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookInstance>> PostBookInstance(BookInstance bookInstance)
        {
          _logger.LogDebug("Received: " + JObject.FromObject(bookInstance).ToString());

            _context.BookInstances.Add(bookInstance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookInstance), new { id = bookInstance.ID }, bookInstance);
        }

        // DELETE: api/copies/5
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
            return _context.BookInstances.Any(e => e.ID == id);
        }
    }
}
