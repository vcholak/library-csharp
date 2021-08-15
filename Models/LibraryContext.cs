using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Models
{
  public class LibraryContext : DbContext
  {
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookInstance> BookInstances { get; set; }
  }
}
