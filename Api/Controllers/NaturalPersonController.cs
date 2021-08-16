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
    /// NaturalPersonController (inherit from PersonController to treat NaturalPerson objects).
    /// </summary>
    [Route("api/naturalperson")]
    public class NaturalPersonController : PersonController<NaturalPerson>
    {
        public NaturalPersonController(
            ILogger<PersonController<NaturalPerson>> logger,
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
        public new Result Insert([FromBody] NaturalPerson person)
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
        public new Result Update([FromBody] NaturalPerson person)
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
        public new Result Delete([FromBody] NaturalPerson person)
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