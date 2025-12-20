namespace Middleware_Auth.Models
{
    public class UserConstants
    {
        public static List<User> Users = new List<User> {
            new User{ UserName = "shreyaww", Email = "shreya@gmail.com", Password = "12345",
                GivenName = "Shreya", Surname = "Singh", Role = "Administrator" },
            new User{ UserName = "harsh_rs", Email = "harsh@gmail.com", Password = "67890",
                GivenName = "Harsh", Surname = "Ranjan", Role = "Seller" }
        };
    }
    
}
