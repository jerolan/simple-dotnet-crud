using System;
using System.Collections.Generic;

namespace TransactionsEventSourcing
{
  public interface IRepository<T>
  {
    IEnumerable<T> GetAll();
    T GetById(Guid id);
    void Insert(T employee);
    void Update(T employee);
    void Delete(Guid id);
    void Save();
  }
}