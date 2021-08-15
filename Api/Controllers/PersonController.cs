using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Services;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    public abstract class PersonController<T> : ControllerBase where T : Person
    {
        protected readonly ILogger<PersonController<T>> _logger;
        protected readonly IPersonService _personService;
        protected readonly IPersonDtoAdapter _dtoAdapter;

        public PersonController(
            ILogger<PersonController<T>> logger,
            IPersonService personService,
            IPersonDtoAdapter dtoAdapter)
        {
            this._logger = logger;
            this._personService = personService;
            this._dtoAdapter = dtoAdapter;
        }

        [HttpGet]
        [Route("getpersons")]
        public Result<IEnumerable<PersonDto>> Get()
        {
            this._logger.LogTrace("Initializing Get(); class: PersonController; layer: Api.");

            var result = new Result<IEnumerable<PersonDto>>();
            var pResult = this._personService.Get();

            if (pResult.HasError)
                result.AddError(pResult.Errors.First());
            else
                result.Content = this._dtoAdapter.Adaptee(pResult.Content);

            this._logger.LogTrace("Finalizing Get(); class: PersonController; layer: Api.");

            return result;
        }

        protected Result Insert(T person)
        {
            this._logger.LogTrace("Initializing Insert(); class: PersonController; layer: Api.");

            var result = this._personService.Insert(person);

            this._logger.LogTrace("Finalizing Insert(); class: PersonController; layer: Api.");

            return result;
        }

        protected Result Update(T person)
        {
            this._logger.LogTrace("Initializing Update(); class: PersonController; layer: Api.");

            var result = this._personService.Update(person);

            this._logger.LogTrace("Finalizing Update(); class: PersonController; layer: Api.");

            return result;
        }

        protected Result Delete(T instance)
        {
            this._logger.LogTrace("Initializing Delete(); class: PersonController; layer: Api.");

            var result = this._personService.Delete(instance);

            this._logger.LogTrace("Finalizing Delete(); class: PersonController; layer: Api.");

            return result;
        }

        protected Result<Person> GetById(int id)
        {
            this._logger.LogTrace("Initializing GetById(); class: PersonController; layer: Api.");

            var result = this._personService.Get(id);

            this._logger.LogTrace("Finalizing GetById(); class: PersonController; layer: Api.");

            return result;
        }
    }
}
