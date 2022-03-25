using Microsoft.AspNetCore.Identity;

namespace BlogApplication.Data.Entities
{
    public class BlogUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
