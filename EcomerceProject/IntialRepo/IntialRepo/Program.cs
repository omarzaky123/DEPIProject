using DEPI.Core.Models;
using IntialRepo.Core.IRepositorys;
using IntialRepo.Core.Repositorys;
using IntialRepo.EF;
using IntialRepo.EF.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IntialRepo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region MyArea

            #region ConnectionString
            string connection = builder.Configuration.GetConnectionString("Local");
            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(connection, B => B.MigrationsAssembly(typeof(MyContext).Assembly.FullName));
            });
            #endregion

            #region Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                // Add other password requirements if needed
                // options.Password.RequiredLength = 8;
                // options.Password.RequireNonAlphanumeric = true;
                // options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<MyContext>()
                .AddDefaultTokenProviders(); // Important for UserManager functionality

            // Configure Application Cookies if needed
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            #region Register
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Mapping
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            #endregion

            #endregion

            builder.Services.AddControllers();
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

            // Add these middleware IN THIS ORDER
            app.UseAuthentication(); // This must come before UseAuthorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}