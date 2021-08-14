using System;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Domain.Results;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ILogger _logger;

        public BaseRepository(ILogger logger)
        {
            this._logger = logger;
        }

        public abstract Result Delete(T instance);

        public abstract Result<IEnumerable<T>> Get();

        public abstract Result Insert(T instance);

        public abstract Result Update(T instance);

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
