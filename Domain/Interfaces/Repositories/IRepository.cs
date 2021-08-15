using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Results;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        Result Insert(T instance);
        Result Update(T instance);
        Result Delete(T instance);
        Result<IEnumerable<T>> Get();
        Result<T> Get(int id);
    }
}