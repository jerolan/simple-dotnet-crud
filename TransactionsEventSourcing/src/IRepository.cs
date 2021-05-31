using System;
using System.Collections.Generic;

namespace TransactionsEventSourcing
{
  public interface IRepository<T>
  {
    T Find();
    IEnumerable<T> Get(T t);
    T GetByID(int tId);
    void Insert(T t);
    void Delete(int tId);
    void Update(T t);
    void Save();
  }
}