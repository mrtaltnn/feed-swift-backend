namespace IdentityService.Domain.Constants;

public class MaxLengths
{
    public static class User
    {
        public const int Email = 320;
        public const int FirstName = 100;
        public const int LastName = 100;
        public const int Password = 128;
        public const int PhoneNumber = 13;
        public const int DeviceId = 15;
    }
}