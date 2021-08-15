using Domain.VOs;

namespace Domain.Results
{
    public sealed class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public Address Address { get; set; }
        public string Suffix { get; set; }
    }
}