using Microsoft.AspNetCore.Identity;

namespace ForumIT.Models.Domain
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
