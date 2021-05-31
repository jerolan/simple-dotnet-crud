using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsEventSourcing
{
  public class InMemoryRepository : IRepository<Transaction>
  {
    private readonly List<Transaction> _store;
    public InMemoryRepository()
    {
      _store = new List<Transaction>();
    }

    public IEnumerable<Transaction> GetAll()
    {
      return _store;
    }

    public Transaction GetById(Guid transactionId)
    {
      return _store.Find(item => item.CustomerId == transactionId);
    }

    public void Insert(Transaction transaction)
    {
      _store.Add(transaction);
    }

    public void Delete(Guid transactionId)
    {
      _store.Remove(new Transaction() { TransactionId = transactionId });
    }

    public void Update(Transaction transaction)
    {
      var index = _store.IndexOf(new Transaction() { TransactionId = transaction.TransactionId });
      var current = _store[index];
      CopyValues(current, transaction);
      _store[index] = current;
    }

    public void Save()
    {
      throw new Exception("No impl");
    }

    private void CopyValues<T>(T target, T source)
    {
      Type t = typeof(T);

      var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

      foreach (var prop in properties)
      {
        var value = prop.GetValue(source, null);
        if (value != null)
          prop.SetValue(target, value, null);
      }
    }
  }
}
