using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
  [Table("authors")]
  public class Author
  {
    [Column("id")]
    public long Id { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("family_name")]
    public string FamilyName { get; set; }

    [Column("birth_date")]
    public DateTime BirthDate { get; set; }

    [Column("death_date")]
    public DateTime? DeathDate { get; set; }

    [Column("life_span")]
    public string LifeSpan { get; set; }

    public ICollection<Book> Books { get; }
  }
}
