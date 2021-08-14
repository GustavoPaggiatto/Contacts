using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Results;
using Microsoft.Extensions.Logging;

namespace Data
{
    public sealed class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        static ConcurrentDictionary<int, Person> _entities;
        static object _lock;

        static PersonRepository()
        {
            _entities = new ConcurrentDictionary<int, Person>();
            _lock = new object();
        }

        public PersonRepository(ILogger<PersonRepository> logger) : base(logger)
        {
        }

        public override Result Delete(Person instance)
        {
            var result = new Result();

            this._logger.LogTrace("Initializing Delete(); class: PersonRepository; layer: Data.");

            try
            {
                var entity = _entities[instance.Id];

                if (entity != null)
                {
                    Person removed;
                    _entities.Remove(instance.Id, out removed);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error while delete Person.", ex);
                result.AddError("There was an error deleting the contact, please try again.");
            }
            finally
            {
                this._logger.LogTrace("Finalizing Delete(); class: PersonRepository; layer: Data.");
            }

            return result;
        }

        public override Result<IEnumerable<Person>> Get()
        {
            var result = new Result<IEnumerable<Person>>();

            this._logger.LogTrace("Initializing Get(); class: PersonRepository; layer: Data.");

            try
            {
                var persons = new List<Person>();

                foreach (Person p in _entities.Values)
                    persons.Add(p.Clone() as Person);

                result.Content = persons;
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error while get Persons.", ex);
                result.AddError("There was an error while getting the contacts, please try again.");
            }
            finally
            {
                this._logger.LogTrace("Finalizing Get(); class: PersonRepository; layer: Data.");
            }

            return result;
        }

        public override Result Insert(Person instance)
        {
            var result = new Result();

            this._logger.LogTrace("Initializing Insert(); class: PersonRepository; layer: Data.");

            try
            {
                int lastId = 0;

                lock (_lock)
                {
                    lastId = _entities.Keys.Max();
                }

                _entities.TryAdd(++lastId, instance);
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error while insert Person.", ex);
                result.AddError("There was an error inserting the contact, please try again.");
            }
            finally
            {
                this._logger.LogTrace("Finalizing Insert(); class: PersonRepository; layer: Data.");
            }

            return result;
        }

        public override Result Update(Person instance)
        {
            var result = new Result();

            this._logger.LogTrace("Initializing Update(); class: PersonRepository; layer: Data.");

            try
            {
                _entities[instance.Id] = instance;
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error while update Person.", ex);
                result.AddError("There was an error updating the contact, please try again.");
            }
            finally
            {
                this._logger.LogTrace("Finalizing Update(); class: PersonRepository; layer: Data.");
            }

            return result;
        }
    }
}