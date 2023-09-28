namespace IdentityService.Application.DTOs.User;

public sealed record CreateUserDto(string Email, string FirstName, string LastName, string Password)
{
    public required string Email { get; set; } = Email;
    public required  string FirstName { get; set; } = FirstName;
    public required  string LastName { get; set; } = LastName;
    public required  string Password { get; set; } = Password;
}