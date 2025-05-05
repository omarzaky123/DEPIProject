using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace DEPIMVC.Controllers
{
    public class AccountController : Controller
    {
        #region Ctor
        private readonly IMangeCookie mangeCookie;
        Uri baseaddress = new Uri("https://localhost:44312/api");
        private readonly HttpClient _client;


        public AccountController(IMangeCookie mangeCookie)
        {
            this.mangeCookie = mangeCookie;
            _client = new HttpClient();
            _client.BaseAddress = baseaddress;
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login(string Url)
        {
            TempData["UrlLogin"] = Url;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var apiUrl = baseaddress+"/Account/Login";
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(apiUrl, model);

            if (response.IsSuccessStatusCode)
            {
                mangeCookie.SetCookie(response,model.RememberMe);
                return RedirectToAction("DetermineDestination");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", errorContent);
            return View(model);
        }
        public IActionResult DetermineDestination()
        {
            if (User.IsInRole("Admin"))
                TempData["UrlLogin"] = "/AdminWelcome/Index";

            if (TempData["UrlLogin"] != null)
            {
                string redirectUrl = TempData["UrlLogin"].ToString();
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Home");

        }

        #endregion

        #region Register
        [HttpGet]
        public IActionResult NewAdmin(string Url="/AdminWelcome/Index")
        {
            TempData["UrlRegister"] = Url;
            return View();
        }
        [HttpGet]
        public IActionResult Register(string Url)
        {
            TempData["UrlRegister"] = Url;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model,string Role="Guset")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiUrl = baseaddress+ $"/Account/Register/{Role}";

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(apiUrl, model);
            if (response.IsSuccessStatusCode)
            { 
                mangeCookie.SetCookie(response);
                if (TempData["UrlRegister"] != null)
                {
                    string redirectUrl = TempData["UrlRegister"].ToString();
                    return Redirect(redirectUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            var errors = JsonSerializer.Deserialize<List<string>>(errorContent);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }
        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
           
            var apiUrl = baseaddress+"/Account/Logout";
            using var httpClient = new HttpClient();
            await httpClient.PostAsync(apiUrl, null);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion

    }
}