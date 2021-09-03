using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
  [Table("genres")]
  public class Genre
  {
    [Column("id")]
    public long ID { get; set; }

    [Column("name")]
    public string Name { get; set; }
  }
}

