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
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(LibraryContext context, ILogger<AuthorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // HEAD api/Authors
        [HttpHead]
        public async Task<ActionResult> Count()
        {
          int count = await _context.Authors.CountAsync();
          Response.Headers.Add("X-Result-Count", count.ToString());

          return NoContent();
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> AllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(long id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(long id, Author author)
        {
            if (id != author.ID)
            {
                return BadRequest();
            }
            _logger.LogDebug(author.ToString());

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
          _logger.LogDebug("Received: " + JObject.FromObject(author).ToString());

          _context.Authors.Add(author);
          await _context.SaveChangesAsync();

          return CreatedAtAction(nameof(GetAuthor), new { id = author.ID }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(long id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(long id)
        {
            return _context.Authors.Any(e => e.ID == id);
        }
    }
}
