using Core.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence;

namespace API.Initializer
{
    public class SampleData
    {
        public static async Task Initialize(RapidPayDbContext dbContext)
        {
            var user = new AppUser
            {
                FirstName = "Test",
                LastName  = "Account",
                Email = "TestAccount@example.com",
                NormalizedEmail = "TESTACCOUNT@EXAMPLE.COM",
                UserName = "TestAccount@example.com",
                NormalizedUserName = "TESTACCOUNT@EXAMPLE.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "string12@A");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                var result = await userStore.CreateAsync(user);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
