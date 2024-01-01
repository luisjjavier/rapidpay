using Microsoft.AspNetCore.Identity;

namespace Core.AppUsers
{
    public  class AppUser: IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
    }
}
