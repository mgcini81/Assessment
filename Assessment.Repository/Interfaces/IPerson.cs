using Assessment.Repository.Entities;

namespace Assessment.Repository.Interfaces
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        Address Address { get; set; }
        string PhoneNumber { get; set; }
    }
}
