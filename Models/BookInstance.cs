using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
  [Table("book_instances")]
  public class BookInstance
  {
    [Column("id")]
    public long ID { get; set; }

    [Column("book_id")]
    public Book Book { get; set; }

    [Column("imprint")]
    public string Imprint { get; set; }

    [Column("status")]
    public BookInstanceStatus Status { get; set; }
    [Column("due_back")]
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
