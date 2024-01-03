using Microsoft.AspNetCore.Identity;

namespace Core.AppUsers
{
    /// <summary>
    /// Represents a user in the RapidPay system.
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [PersonalData]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [PersonalData]
        public string? LastName { get; set; }
    }

}
