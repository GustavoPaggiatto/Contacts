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
    /// <summary>
    /// PersonRepository class is responsible to persist person objects. In this case de persistency is
    /// im memory.
    /// </summary>
    public sealed class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        static ConcurrentDictionary<int, Person> _entities;
        static object _lock;

        /// <summary>
        /// Static constructor to initialize thread safe Dictionary of persisted entities.
        /// </summary>
        static PersonRepository()
        {
            _entities = new ConcurrentDictionary<int, Person>();
            _lock = new object();
        }

        /// <summary>
        /// Constructor tha receive logger instance.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public PersonRepository(ILogger<PersonRepository> logger) : base(logger)
        {
        }

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result Delete(Person instance)
        {
            var result = new Result();

            this._logger.LogTrace("Initializing Delete(); class: PersonRepository; layer: Data.");

            try
            {
                if (_entities.Count > 0)
                {
                    var entity = _entities[instance.Id];

                    if (entity != null)
                    {
                        Person removed;
                        _entities.Remove(instance.Id, out removed);
                    }
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

        /// <summary>
        /// Get method tha return all entities.
        /// </summary>
        /// <returns></returns>
        public override Result<IEnumerable<Person>> Get()
        {
            var result = new Result<IEnumerable<Person>>();

            this._logger.LogTrace("Initializing Get(); class: PersonRepository; layer: Data.");

            try
            {
                var persons = new List<Person>();

                foreach (int pId in _entities.Keys)
                {
                    var person = _entities[pId].Clone() as Person;

                    person.Id = pId;
                    persons.Add(person);
                }

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

        /// <summary>
        /// Get overload method (by id).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Result<Person> Get(int id)
        {
            var result = new Result<Person>();

            this._logger.LogTrace("Initializing Get(); class: PersonRepository; layer: Data.");

            try
            {
                var persons = new List<Person>();

                if (_entities.Count > 0)
                {
                    result.Content = _entities[id];
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error while get Person by Id.", ex);
                result.AddError("There was an error while getting the contact by Id, please try again.");
            }
            finally
            {
                this._logger.LogTrace("Finalizing Get(); class: PersonRepository; layer: Data.");
            }

            return result;
        }

        /// <summary>
        /// Insert method.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result Insert(Person instance)
        {
            var result = new Result();

            this._logger.LogTrace("Initializing Insert(); class: PersonRepository; layer: Data.");

            try
            {
                int lastId = 0;

                lock (_lock)
                {
                    if (_entities.Count > 0)
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

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
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