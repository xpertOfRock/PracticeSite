namespace PracticeSite.Models.ValueObjects
{
    public class Address
    {
        public string State { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        public Address(string state, string city, string street, string postalCode)
        {
            State = state;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }

        public override string ToString() => $"{State}, {City}, {Street}, {PostalCode}";
    }
}
