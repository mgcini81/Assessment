using Assessment.Repository.Interfaces;

namespace Assessment.Repository.Entities
{
    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
