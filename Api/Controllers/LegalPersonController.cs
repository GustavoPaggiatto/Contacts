using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Services;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    /// <summary>
    /// LegalPersonController (inherit from PersonController to treat LegalPerson objects).
    /// </summary>
    [Route("api/legalperson")]
    public class LegalPersonController : PersonController<LegalPerson>
    {
        public LegalPersonController(
            ILogger<PersonController<LegalPerson>> logger,
            IPersonService personService,
            IPersonDtoAdapter dtoAdapter) : base(logger, personService, dtoAdapter)
        {
        }

        /// <summary>
        /// Insert method.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")]
        public new Result Insert([FromBody] LegalPerson person)
        {
            return base.Insert(person);
        }

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public new Result Update([FromBody] LegalPerson person)
        {
            return base.Update(person);
        }

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public new Result Delete([FromBody] LegalPerson person)
        {
            return base.Delete(person);
        }

        /// <summary>
        /// Get by Id (code) method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbyid")]
        public new Result<LegalPerson> GetById(int id)
        {
            var result = base.GetById(id);
            var legalResult = new Result<LegalPerson>();

            legalResult.Content = result.Content as LegalPerson;

            if (result.HasError)
                legalResult.AddError(result.Errors.First());

            return legalResult;
        }
    }
}