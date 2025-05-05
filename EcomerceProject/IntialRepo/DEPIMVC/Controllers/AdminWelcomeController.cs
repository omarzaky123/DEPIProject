using Microsoft.AspNetCore.Mvc;

namespace DEPIMVC.Controllers
{
    public class AdminWelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
