using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Services;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/naturalperson")]
    public class NaturalPersonController : PersonController<NaturalPerson>
    {
        public NaturalPersonController(
            ILogger<PersonController<NaturalPerson>> logger,
            IPersonService personService,
            IPersonDtoAdapter dtoAdapter) : base(logger, personService, dtoAdapter)
        {
        }

        [HttpPost]
        [Route("insert")]
        public new Result Insert([FromBody] NaturalPerson person)
        {
            return base.Insert(person);
        }

        [HttpPost]
        [Route("update")]
        public new Result Update([FromBody] NaturalPerson person)
        {
            return base.Update(person);
        }

        [HttpPost]
        [Route("delete")]
        public new Result Delete([FromBody] NaturalPerson person)
        {
            return base.Delete(person);
        }

        [HttpGet]
        [Route("getbyid")]
        public new Result<NaturalPerson> GetById(int id)
        {
            var result = base.GetById(id);
            var naturalResult = new Result<NaturalPerson>();

            naturalResult.Content = result.Content as NaturalPerson;

            if (result.HasError)
                naturalResult.AddError(result.Errors.First());

            return naturalResult;
        }
    }
}