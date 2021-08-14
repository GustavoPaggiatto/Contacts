namespace Domain.VOs
{
    public sealed class Address
    {
        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Line { get; private set; }
        public string ZipCode { get; private set; }

        public Address(
            string country,
            string state,
            string city,
            string line,
            string zipCode)
        {
            this.City = city;
            this.Country = country;
            this.Line = line;
            this.State = state;
            this.ZipCode = zipCode;
        }
    }
}