using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
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

        public PersonController(ILogger<PersonController<T>> logger, IPersonService personService)
        {
            _logger = logger;
            this._personService = personService;
        }

        [HttpGet]
        [Route("api/getpersons")]
        public JsonResult Get()
        {
            this._logger.LogTrace("Initializing Get(); class: PersonController; layer: Api.");

            var result = this._personService.Get();

            this._logger.LogTrace("Finalizing Get(); class: PersonController; layer: Api.");

            return new JsonResult(result, new JsonSerializerOptions()
            {
                IgnoreReadOnlyFields = false,
                IgnoreReadOnlyProperties = false
            });
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
    }
}
