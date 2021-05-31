using System;
using System.Collections.Generic;

namespace TransactionsEventSourcing
{
  public class InMemoryRepository : IRepository<Transaction>
  {
    public Transaction Find()
    {
      throw new Exception("No impl");
    }

    public IEnumerable<Transaction> Get(Transaction transaction)
    {
      var list = new List<Transaction>();

      list.Add(
        new Transaction()
        {
          Amount = 10
        }
      );

      list.Add(
        new Transaction()
        {
          Amount = 20
        }
      );

      return list;
    }

    public Transaction GetByID(int tId)
    {
      throw new Exception("No impl");
    }

    public void Insert(Transaction transaction)
    {
      throw new Exception("No impl");
    }

    public void Delete(int tId)
    {
      throw new Exception("No impl");
    }

    public void Update(Transaction transaction)
    {
      throw new Exception("No impl");
    }

    public void Save()
    {
      throw new Exception("No impl");
    }
  }
}
