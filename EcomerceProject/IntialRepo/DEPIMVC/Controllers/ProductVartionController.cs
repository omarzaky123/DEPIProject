using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DEPIMVC.Controllers
{
    public class ProductVartionController : Controller
    {
        private readonly IApiCall<ProductVationInsertVm> apiCallPrdVartionInsert;
        private readonly IApiCall<ProductVationVm> apiCallPrdVartion;
        private readonly IApiCall<ProductVm> apiCallProduct;
        private readonly IApiCall<VartionInsertVm> apiCallVartion;

        public ProductVartionController(IApiCall<ProductVationInsertVm> apiCallPrdVartionInsert,
            IApiCall<ProductVationVm> apiCallPrdVartion,
            IApiCall<ProductVm> apiCallProduct,
            IApiCall<VartionInsertVm> apiCallVartion
            )
        {
            this.apiCallPrdVartionInsert = apiCallPrdVartionInsert;
            this.apiCallPrdVartion = apiCallPrdVartion;
            this.apiCallProduct = apiCallProduct;
            this.apiCallVartion = apiCallVartion;
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            List<ProductVationVm> productVationVms = await apiCallPrdVartion.GetAll("/ProductVartion/GetAllWithPrd");
            ViewBag.ProductVarions = productVationVms;
            ViewBag.Products = await apiCallProduct.GetAll("/Product/GetAll");
            ViewBag.Vartions = new List<VartionInsertVm>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ProductVationInsertVm productInsertVm)
        {
            if (ModelState.IsValid)
            {

                await apiCallPrdVartionInsert.Insert("/ProductVartion/Insert", productInsertVm);
                return RedirectToAction("Insert");
            }
            return View(productInsertVm);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                ProductVationInsertVm productInsertVm = await apiCallPrdVartionInsert.GetById($"/ProductVartion/GetById/{id}");
                if (productInsertVm != null)
                {
                    List<VartionInsertVm> vartionInsertVms = new List<VartionInsertVm>();
                    ViewBag.Products = await apiCallProduct.GetAll("/Product/GetAll");
                    VartionInsertVm vartionInsertVm =  await apiCallVartion.GetById($"/Vartion/GetByProductId/{productInsertVm.ProductID}");
                    vartionInsertVms.Add(vartionInsertVm);
                    ViewBag.Vartions = vartionInsertVms;
                    return View(productInsertVm);
                }
            }
            return Content("Falid To Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVationInsertVm productInsertVm)
        {
            if (ModelState.IsValid)
            {
                await apiCallPrdVartionInsert.Update("/ProductVartion/Edit", productInsertVm);
                return RedirectToAction("Insert");
            }
            return View(productInsertVm);
        }




        public async Task<IActionResult> Delete(int id)
        {

            if (id != 0)
            {
                bool state = await apiCallPrdVartionInsert.Delete("/ProductVartion/Delete", id.ToString());
                if (state)
                {
                    return RedirectToAction("Insert");
                }
            }
            return Content("Falid To delete");
        }

        public async Task<IActionResult> Details(int id)
        {
            ProductVationVm productVm = await apiCallPrdVartion.GetById($"/ProductVartion/GetByIdWithPrd/{id}");
            return View(productVm);
        }
    }
}
