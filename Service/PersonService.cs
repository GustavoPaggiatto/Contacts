using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Service
{
    public class PersonService : BaseService<Person>, IPersonService
    {
        public PersonService(IPersonRepository repository, ILogger<PersonService> logger) : base(repository, logger)
        {
        }
    }
}