
using API.Initializer;
using Core;
using Core.AppUsers;
using Core.Cards;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            BuildDbConfiguration(builder);

            builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
            builder.Services.AddAuthorizationBuilder();
            builder.Services.AddScoped<IRepository<Card>, RapidPayRepository<Card>>();
            builder.Services.AddScoped<ICardService,CardService>();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapIdentityApi<AppUser>();

            await InitializeDb(app);

            await app.RunAsync();
        }

        private static async Task InitializeDb(WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<RapidPayDbContext>();
            await dbContext.Database.MigrateAsync();
            await dbContext.Database.EnsureCreatedAsync();

            await SampleData.Initialize(dbContext);
        }

        private static void BuildDbConfiguration(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RapidPayDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), optionsBuilder =>
                {
                    optionsBuilder.CommandTimeout(300);
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });

            builder.Services.AddIdentityCore<AppUser>()
                .AddEntityFrameworkStores<RapidPayDbContext>()
                .AddApiEndpoints();
        }
    }
}
