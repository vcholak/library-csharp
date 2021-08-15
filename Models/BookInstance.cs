using System;

namespace Library.Models
{
  public class BookInstance
  {
    public long Id { get; set; }
    public Book Book { get; set; }
    public string Imprint { get; set; }
    public BookInstanceStatus Status { get; set; }
    public DateTime DueBack { get; set; }
  }

  public enum BookInstanceStatus
  {
    NotAvailable,
    OnOrder,
    InTransit,
    OnHold,
    OnLoan,
    InLibrary
  }
}
