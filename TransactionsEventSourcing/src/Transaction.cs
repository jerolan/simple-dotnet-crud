using System;

namespace TransactionsEventSourcing
{
  public class Transaction
  {
    public decimal Amount { get; set; }
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }

    public Transaction()
    {
      TransactionId = new Guid();
    }
  }
}