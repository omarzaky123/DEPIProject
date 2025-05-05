using DEPIMVC.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DEPIMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            #region Register
            builder.Services.AddScoped(typeof(IApiCall<>), typeof(ApiCall<>));
            builder.Services.AddScoped<IMangeCookie, MangeCookie>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddHttpContextAccessor();

            #endregion
            #region Cookie
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                 options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";

                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;

            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
