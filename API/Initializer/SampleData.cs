using API.Models;
using AutoMapper;
using Core.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence;
using System.Text.Json;

namespace API.Initializer
{
    /// <summary>
    /// Class responsible for initializing sample data in the RapidPayDbContext.
    /// </summary>
    public class SampleData
    {
        /// <summary>
        /// Initializes sample data in the specified RapidPayDbContext using the provided IMapper.
        /// </summary>
        /// <param name="dbContext">The RapidPayDbContext instance to initialize.</param>
        /// <param name="mapper">The IMapper instance for mapping data.</param>
        public static async Task Initialize(RapidPayDbContext dbContext, IMapper mapper)
        {
            try
            {
                await TryToCreateDefaultUser(dbContext, mapper);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        
        }

        private static async Task TryToCreateDefaultUser(RapidPayDbContext dbContext, IMapper mapper)
        {
            // Read raw JSON data from the default-user.json file.
            var rawJson = await File.ReadAllTextAsync("./initializer/default-user.json");

            // Deserialize the JSON data into a UserDto object.
            var userDto = JsonSerializer.Deserialize<UserDto>(rawJson);

            // Map the UserDto to an AppUser.
            var user = mapper.Map<AppUser>(userDto);

            // Generate a new security stamp for the user.
            user.SecurityStamp = Guid.NewGuid().ToString("D");

            // Check if a user with the same username already exists in the database.
            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                // Hash the user's password.
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, userDto.Password);
                user.PasswordHash = hashed;

                // Create the user using the UserStore.
                var userStore = new UserStore<AppUser>(dbContext);
                var result = await userStore.CreateAsync(user);

                // Save changes to the database.
                await dbContext.SaveChangesAsync();
            }
        }
    }

}
