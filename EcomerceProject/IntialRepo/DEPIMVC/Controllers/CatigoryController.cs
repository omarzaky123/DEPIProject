using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPIMVC.Controllers
{
    public class CatigoryController : Controller
    {
        private readonly IApiCall<CatigoryVm> apiCall;
        private readonly IApiCall<CatigoryWithPrdsVm> apiCallWithPRd;
        private readonly IImageRepository imageRepository;

        public CatigoryController(IApiCall<CatigoryVm> apiCall,IApiCall<CatigoryWithPrdsVm> apiCallWithPRd,IImageRepository imageRepository)
        {
            this.apiCall = apiCall;
            this.apiCallWithPRd = apiCallWithPRd;
            this.imageRepository = imageRepository;
        }
        public async Task<IActionResult> GetById(int id)
        {
            if (id != 0)
            {
                CatigoryWithPrdsVm catigoryVm = await apiCallWithPRd.GetById($"/Catigory/GetByIdWithRelatedPrd/{id}");
                if (catigoryVm != null)
                {
                    return View(catigoryVm);
                }
            }
            return Content("Falid To Edit");

        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            ViewBag.catigoryVms = await apiCall.GetAll("/Catigory/Get");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CatigoryVm catigoryVm) {

            if (ModelState.IsValid) {
                await imageRepository.SaveImageInWroot(catigoryVm, catigoryVm.ImageFile);
                await apiCall.Insert("/Catigory/Insert", catigoryVm);
                return RedirectToAction("Insert");
            }
            return View(catigoryVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {

            if (id != 0) {
               CatigoryVm catigoryVm = await apiCall.GetById($"/Catigory/GetById/{id}");
                if (catigoryVm != null) {
                    return View(catigoryVm);
                }
            }
            return Content("Falid To Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CatigoryVm catigoryVm)
        {
                if (ModelState.IsValid)
                {
                CatigoryVm Oldcatigory = await apiCall.GetById($"/Catigory/GetById/{catigoryVm.Id}");
                await imageRepository.UpdateImageInWroot(Oldcatigory.Image, catigoryVm, catigoryVm.ImageFile);
                await apiCall.Update("/Catigory/Edit", catigoryVm);
                    return RedirectToAction("Insert");
                }
            return View(catigoryVm);
        }

        public async Task<IActionResult> Delete(int id) {

            if (id != 0) {
                CatigoryVm catigoryVm = await apiCall.GetById($"/Catigory/GetById/{id}");
                await imageRepository.DeleteImageInWroot(catigoryVm.Image);
                bool state = await apiCall.Delete("/Catigory/Delete", id.ToString());
                if (state) {
                    return RedirectToAction("Insert");
                }
            }
            return Content("Falid To delete");
        }
    }
}
