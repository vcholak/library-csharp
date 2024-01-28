using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
  [Table("books")]
  public class Book
  {
    [Column("id")]
    public long ID { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("summary")]
    public string Summary { get; set; }

    [Column("isbn")]
    public string ISBN { get; set; }

    [Column("author_id")]
    public long AuthorId { get; set; }

    public Author Author { get; set; }

    [Column("genre_id")]
    public long GenreId { get; set; }

    public Genre Genre { get; set; }
  }
}
