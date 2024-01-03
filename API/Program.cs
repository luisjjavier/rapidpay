using API.Initializer;
using Core;
using Core.AppUsers;
using Core.Cards;
using Core.PaymentFees;
using Core.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API
{
    /// <summary>
    /// Entry point class for the RapidPay web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method to start the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            BuildDbConfiguration(builder);

            builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
            builder.Services.AddAuthorizationBuilder();
            BuildRepositories(builder);
            BuildServices(builder);
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Rapidpay API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

            });

            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapIdentityApi<AppUser>();

            await InitializeDb(app);

            await app.RunAsync();
        }

        private static void BuildServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddSingleton<IPaymentFeeService, PaymentService>();
        }

        private static void BuildRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository<Card>, RapidPayRepository<Card>>();
            builder.Services.AddScoped<IRepository<Transaction>, RapidPayRepository<Transaction>>();

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
