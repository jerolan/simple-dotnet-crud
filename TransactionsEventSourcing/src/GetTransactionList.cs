using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsEventSourcing
{
  public class GetTransactionList
  {
    private readonly IRepository<Transaction> _repository;
    public GetTransactionList(IRepository<Transaction> repository)
    {
      _repository = repository;
    }

    public List<Transaction> Handle()
    {
      var list = _repository.GetAll();
      return list.ToList();
    }
  }
}
