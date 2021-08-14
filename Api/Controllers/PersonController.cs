using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            this._personService = personService;
        }

        [HttpGet]
        [Route("get")]
        public Result<IEnumerable<Person>> Get()
        {
            this._logger.LogTrace("Initializing Get(); class: PersonController; layer: Api.");

            var result = this._personService.Get();

            this._logger.LogTrace("Finalizing Get(); class: PersonController; layer: Api.");

            return result;
        }

        [HttpPost]
        [Route("insert")]
        public Result Insert(Person person)
        {
            this._logger.LogTrace("Initializing Insert(); class: PersonController; layer: Api.");

            var result = this._personService.Insert(person);

            this._logger.LogTrace("Finalizing Insert(); class: PersonController; layer: Api.");

            return result;
        }

        [HttpPost]
        [Route("update")]
        public Result Update(Person person)
        {
            this._logger.LogTrace("Initializing Update(); class: PersonController; layer: Api.");

            var result = this._personService.Update(person);

            this._logger.LogTrace("Finalizing Update(); class: PersonController; layer: Api.");

            return result;
        }

        [HttpPost]
        [Route("delete")]
        public Result Delete(Person instance)
        {
            this._logger.LogTrace("Initializing Delete(); class: PersonController; layer: Api.");

            var result = this._personService.Delete(instance);

            this._logger.LogTrace("Finalizing Delete(); class: PersonController; layer: Api.");

            return result;
        }
    }
}
