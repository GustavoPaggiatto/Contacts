using Domain.Results;
using Domain.VOs;

namespace Domain.Entities
{
    public abstract class Person : BaseEntity
    {
        public Address Address { get; set; }
        public string Document { get; set; }
        public int AddressNumber { get; set; }
        public string AddressComplement { get; set; }
    }
}