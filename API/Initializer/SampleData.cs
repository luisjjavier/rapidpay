using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence;

namespace API.Initializer
{
    public class SampleData
    {
        public static async Task Initialize(RapidPayDbContext dbContext)
        {
            var user = new IdentityUser
            {
                Email = "TestAccount@example.com",
                NormalizedEmail = "TESTACCOUNT@EXAMPLE.COM",
                UserName = "TestAccount",
                NormalizedUserName = "OWNER",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "strongSecret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<IdentityUser>(dbContext);
                var result = await userStore.CreateAsync(user);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
