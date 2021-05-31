using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsEventSourcing
{
  public class GetTransactionListQuery
  {
    public Guid CustomerId { get; }

    public GetTransactionListQuery()
    {
      CustomerId = new Guid();
    }
  }

  public class GetTransactionList
  {
    private readonly IRepository<Transaction> _repository;
    public GetTransactionList(IRepository<Transaction> repository)
    {
      _repository = repository;
    }

    public List<Transaction> Handle(GetTransactionListQuery request)
    {
      var filter = new Transaction() { CustomerId = request.CustomerId };
      var list = _repository.Get(filter);
      return list.ToList();
    }
  }
}
