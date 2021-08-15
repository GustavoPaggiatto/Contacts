using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Visitors;
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
            IPersonJsonVisitor personJsonVisitor) : base(logger, personService, personJsonVisitor)
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
    }
}