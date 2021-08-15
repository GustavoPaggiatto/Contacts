using System.Collections.Generic;
using Domain.Entities;
using Domain.Results;

namespace Domain.Interfaces.Adapters
{
    public interface IPersonDtoAdapter : IAdapter<Person, PersonDto>
    {
    }
}