using System;
using System.Collections.Generic;

namespace Library.Models
{
  public class Author
  {
    public long ID { get; set; }
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime DeathDate { get; set; }
    public string LifeSpan { get; set; }
    public ICollection<Book> Books { get; set; }
  }
}
