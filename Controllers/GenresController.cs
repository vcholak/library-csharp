using System.Diagnostics;
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
    public class GenresController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly ILogger<GenresController> _logger;

        public GenresController(LibraryContext context, ILogger<GenresController> logger)
        {
          _context = context;
          _logger = logger;
        }

        // HEAD api/genres
        [HttpHead]
        public async Task<ActionResult> Count()
        {
          int count = await _context.Genres.CountAsync();
          Response.Headers.Add("X-Result-Count", count.ToString());

          return NoContent();
        }

        // GET: api/genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> AllGenres()
        {
          return await _context.Genres.ToListAsync();
        }

        // GET: api/genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(long id)
        {
          var genre = await _context.Genres.FindAsync(id);

          if (genre == null)
          {
              return NotFound();
          }

          return genre;
        }

        // PUT: api/genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(long id, Genre genre)
        {
          if (id != genre.ID)
          {
            return BadRequest();
          }

          _context.Entry(genre).State = EntityState.Modified;

          try
          {
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {
            if (!GenreExists(id))
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

        // POST: api/genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
          _logger.LogDebug("Received: " + JObject.FromObject(genre).ToString());

          _context.Genres.Add(genre);
          await _context.SaveChangesAsync();

          return CreatedAtAction(nameof(GetGenre), new { id = genre.ID }, genre);
        }

        // DELETE: api/genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(long id)
        {
          var genre = await _context.Genres.FindAsync(id);
          if (genre == null)
          {
            return NotFound();
          }

          _context.Genres.Remove(genre);
          await _context.SaveChangesAsync();

          return NoContent();
        }

        private bool GenreExists(long id)
        {
          return _context.Genres.Any(e => e.ID == id);
        }
    }
}
