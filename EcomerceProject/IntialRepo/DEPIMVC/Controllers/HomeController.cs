using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DEPIMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiCall<ProductVm> _apiCallProductVm;
        private readonly IApiCall<CatigoryVm> _apiCallCatigoryVm;

        public HomeController(ILogger<HomeController> logger
            ,IApiCall<ProductVm> apiCallProductVm,IApiCall<CatigoryVm> apiCallCatigoryVm)
        {
            _logger = logger;
            _apiCallProductVm = apiCallProductVm;
            _apiCallCatigoryVm = apiCallCatigoryVm;
        }

        public async Task<IActionResult> Index()  
        {
            List<ProductVm> products = await _apiCallProductVm.GetAll("/Product/Get");
            List<CatigoryVm> catigoryVms = await _apiCallCatigoryVm.GetAll("/Catigory/Get");
            ViewBag.Products = products;
            ViewBag.Catigorys = catigoryVms;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
