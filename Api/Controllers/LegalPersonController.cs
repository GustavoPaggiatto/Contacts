using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Services;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/legalperson")]
    public class LegalPersonController : PersonController<LegalPerson>
    {
        public LegalPersonController(
            ILogger<PersonController<LegalPerson>> logger,
            IPersonService personService,
            IPersonDtoAdapter dtoAdapter) : base(logger, personService, dtoAdapter)
        {
        }

        [HttpPost]
        [Route("insert")]
        public new Result Insert([FromBody] LegalPerson person)
        {
            return base.Insert(person);
        }

        [HttpPost]
        [Route("update")]
        public new Result Update([FromBody] LegalPerson person)
        {
            return base.Update(person);
        }

        [HttpPost]
        [Route("delete")]
        public new Result Delete([FromBody] LegalPerson person)
        {
            return base.Delete(person);
        }
    }
}