namespace Library.Models
{
  public class Book
  {
    public long ID { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string ISBN { get; set; }
    public Author Author { get; set; }
    public Genre Genre { get; set; }
  }
}
