using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Results;
using Microsoft.Extensions.Logging;

namespace Service
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly ILogger<IService<T>> _logger;

        public BaseService(IRepository<T> repository, ILogger<IService<T>> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public virtual Result Insert(T instance)
        {
            this._logger.LogTrace("Initializing Insert(); class: BaseService; layer: Service.");

            Result result = null;

            if (instance != null)
            {
                var validEntity = instance.IsValid();

                if (validEntity.HasError)
                {
                    result = new Result();

                    foreach (string error in validEntity.Errors)
                        result.AddError(error);
                }
                else
                    result = this._repository.Insert(instance);
            }
            else
                result = new Result();

            this._logger.LogTrace("Finalizing Insert(); class: BaseService; layer: Service.");

            return result;
        }

        public virtual Result Update(T instance)
        {
            this._logger.LogTrace("Initializing Update(); class: BaseService; layer: Service.");

            Result result = null;

            if (instance != null)
            {
                var validEntity = instance.IsValid();

                if (validEntity.HasError)
                {
                    result = new Result();

                    foreach (string error in validEntity.Errors)
                        result.AddError(error);
                }
                else
                {
                    if (instance.Id <= 0)
                    {
                        result = new Result();
                        result.AddError("Register not found.");

                        return result;
                    }

                    result = this._repository.Update(instance);
                }
            }
            else
                result = new Result();

            this._logger.LogTrace("Finalizing Update(); class: BaseService; layer: Service.");

            return result;
        }

        public virtual Result Delete(T instance)
        {
            this._logger.LogTrace("Initializing Delete(); class: BaseService; layer: Service.");
            Result result = null;

            if (instance != null)
            {
                if (instance.Id <= 0)
                {
                    result = new Result();
                    result.AddError("Register not found.");
                    
                    return result;
                }

                result = this._repository.Delete(instance);
            }
            else
                result = new Result();

            this._logger.LogTrace("Finalizing Delete(); class: BaseService; layer: Service.");

            return result;
        }

        public virtual Result<IEnumerable<T>> Get()
        {
            this._logger.LogTrace("Initializing Get(); class: BaseService; layer: Service.");

            var result = this._repository.Get();

            this._logger.LogTrace("Finalizing Delete(); class: BaseService; layer: Service.");

            return result;
        }

        public virtual void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
