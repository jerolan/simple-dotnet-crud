using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsEventSourcing
{
  public class CreateTransactionCommand
  {
    public decimal Amount { get; set; }
  }

  public class CreateTransaction
  {
    private readonly IRepository<Transaction> _repository;

    public CreateTransaction(IRepository<Transaction> repository)
    {
      _repository = repository;
    }

    public Transaction Handle(CreateTransactionCommand request)
    {
      var createdTransaction = new Transaction() { Amount = request.Amount };
      _repository.Insert(createdTransaction);
      return createdTransaction;
    }
  }
}
