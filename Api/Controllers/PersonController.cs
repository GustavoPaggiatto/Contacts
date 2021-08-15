using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Visitors;
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
        protected readonly IPersonJsonVisitor _jsonVisitor;

        public PersonController(
            ILogger<PersonController<T>> logger,
            IPersonService personService,
            IPersonJsonVisitor jsonVisitor)
        {
            this._logger = logger;
            this._personService = personService;
            this._jsonVisitor = jsonVisitor;
        }

        [HttpGet]
        [Route("api/getpersons")]
        public Result<string> Get()
        {
            try
            {
                this._logger.LogTrace("Initializing Get(); class: PersonController; layer: Api.");

                var result = new Result<string>();
                var pResult = this._personService.Get();

                if (result.HasError)
                {
                    result.AddError(result.Errors.First());
                    return result;
                }
                else
                {
                    string content = "[";

                    foreach (var person in pResult.Content)
                    {
                        var vResult = this._jsonVisitor.Visit(person);

                        if (vResult.HasError)
                        {
                            result.AddError(vResult.Errors.First());
                            return result;
                        }
                        else
                        {
                            content += this._jsonVisitor.GetJson() + ",";
                        }
                    }

                    content = content.Substring(0, content.Length - 1);
                    content += "]";
                    result.Content = content;

                    return result;
                }
            }
            finally
            {
                this._logger.LogTrace("Finalizing Get(); class: PersonController; layer: Api.");
            }
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
