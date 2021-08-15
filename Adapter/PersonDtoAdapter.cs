using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Results;

namespace Adapter
{
    public sealed class PersonDtoAdapter : BaseAdapter<Person, PersonDto>, IPersonDtoAdapter
    {
        public override IEnumerable<PersonDto> Adaptee(IEnumerable<Person> inputs)
        {
            var dtos = base.Adaptee(inputs);

            foreach (var person in inputs)
            {
                var dto = dtos.First(i => i.Id == person.Id);

                dto.Name = person is LegalPerson ?
                    (person as LegalPerson).CompanyName :
                    (person as NaturalPerson).Name;

                dto.Suffix = person is LegalPerson ?
                    "legalperson" :
                    "naturalperson";
            }

            return dtos;
        }
    }
}