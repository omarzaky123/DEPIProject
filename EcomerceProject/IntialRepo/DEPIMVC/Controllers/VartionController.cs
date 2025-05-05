using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DEPIMVC.Controllers
{
    public class VartionController : Controller
    {
        private readonly IApiCall<VartionInsertVm> apiCall;
        private readonly IApiCall<VartionWithVm> apiCallVartionWith;
        private readonly IApiCall<CatigoryVm> apiCallCatigory;

        public VartionController(IApiCall<VartionInsertVm> apiCall,IApiCall<VartionWithVm> apiCallVartionWith
            ,IApiCall<CatigoryVm> apiCallCatigory)
        {
            this.apiCall = apiCall;
            this.apiCallVartionWith = apiCallVartionWith;
            this.apiCallCatigory = apiCallCatigory;
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {

            //GetAll
            List<VartionWithVm> VartionsVms = await apiCallVartionWith.GetAll("/Vartion/GetWithCatigory");
            ViewBag.Vartions = VartionsVms;
            ViewBag.Catigorys = await apiCallCatigory.GetAll("/Catigory/Get");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(VartionInsertVm vartionInsertVm)
        {
            //Insert
            if (ModelState.IsValid)
            {
                await apiCall.Insert("/Vartion/Insert", vartionInsertVm);
                return RedirectToAction("Insert");
            }
            return View(vartionInsertVm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            if (id != 0)
            {
                VartionInsertVm vartionInsertVm = await apiCall.GetById($"/Vartion/GetById/{id}");
                if (vartionInsertVm != null)
                {
                    ViewBag.Catigorys = await apiCallCatigory.GetAll("/Catigory/Get");
                    return View(vartionInsertVm);
                }
            }
            return Content("Falid To Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VartionInsertVm vartionInsertVm)
        {
            if (ModelState.IsValid)
            {
                await apiCall.Update("/Vartion/Edit", vartionInsertVm);
                return RedirectToAction("Insert");
            }
            return View(vartionInsertVm);
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                bool state = await apiCall.Delete("/Vartion/Delete", id.ToString());
                if (state)
                {
                    return RedirectToAction("Insert");
                }
            }
            return Content("Falid To delete");
        }
        public async Task<IActionResult> Details(int id)
        {
            VartionWithVm vartionWithVm = await apiCallVartionWith.GetById($"/Vartion/GetById/{id}");
            return View(vartionWithVm);
        }

        public async Task<IActionResult> GetByProductId(int productId)
        {
            VartionInsertVm vm = await apiCall.GetById($"/Vartion/GetByProductId/{productId}");
            if (vm != null)
                return Ok(vm);    
            return BadRequest("The vm Is Null");
        }



    }
}
