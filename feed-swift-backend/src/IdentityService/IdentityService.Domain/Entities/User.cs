
using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Entities;

public class User:BaseEntity
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; }
    
    public UserRole Role { get; private set; }
    public string DeviceId { get; private set; }
    public DateTimeOffset LastLoggedInDate { get; private set; }
    


    public string FullName => $"{FirstName} {LastName}";

    public User(int id, string email, string firstName, string lastName, string password, string phoneNumber,UserRole role, string deviceId) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        PhoneNumber = phoneNumber;
        Role = role;
        DeviceId = deviceId;
    }
}