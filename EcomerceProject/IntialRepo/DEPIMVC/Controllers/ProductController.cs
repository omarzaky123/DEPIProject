using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DEPIMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IApiCall<ProductVm> apiCall;
        private readonly IApiCall<GusetVm> apiCallGuset;
        private readonly IApiCall<ShoppingCartVm> apiCallShoppingCart;
        private readonly IApiCall<CatigoryVm> apiCallCatigory;
        private readonly IApiCall<ProductInsertVm> apiCallInsert;
        private readonly IImageRepository imageRepository;

        public ProductController(IApiCall<ProductVm> apiCall
            ,IApiCall<GusetVm> apiCallGuset
            ,IApiCall<ShoppingCartVm> apiCallShoppingCart
            ,IApiCall<CatigoryVm> apiCallCatigory,IApiCall<ProductInsertVm> apiCallInsert,
            IImageRepository imageRepository)
        {
            this.apiCall = apiCall;
            this.apiCallGuset = apiCallGuset;
            this.apiCallShoppingCart = apiCallShoppingCart;
            this.apiCallCatigory= apiCallCatigory;
            this.apiCallInsert = apiCallInsert;
            this.imageRepository = imageRepository;
        }

        public async Task<IActionResult> Shop()
        {
            List<ProductVm> productVms = await apiCall.GetAll("/Product/GetAll");
            return View(productVms);
        }

        public async Task<IActionResult> GetById(int id)
        {
            ProductVm productVm = await apiCall.GetById($"/Product/{id}");
            return View(productVm);
        }

        [HttpGet]
        public async Task<IActionResult> Insert() {

            //GetAll
            List<ProductVm> productVms = await apiCall.GetAll("/Product/GetAll");
            ViewBag.Products = productVms;
            ViewBag.Catigorys =await apiCallCatigory.GetAll("/Catigory/Get");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Insert the Product With Its Related Many Images
        public async Task<IActionResult> Insert(ProductInsertVm productInsertVm)
        {
            List<ProductImageInsertVm> productImages=new List<ProductImageInsertVm>();  
            if (ModelState.IsValid) {
                await imageRepository.SaveImageInWroot(productInsertVm, productInsertVm.ImageFile);
                for (int i = 0;i< productInsertVm.ProductImagesFiles.Count; i++)
                {
                    ProductImageInsertVm productImageInsertVm=new ProductImageInsertVm();
                    productImageInsertVm.ProductId = productInsertVm.Id;
                    await imageRepository.SaveImageInWroot(productImageInsertVm, productInsertVm.ProductImagesFiles[i]);
                    productImages.Add(productImageInsertVm);
                }
                productInsertVm.ProductImages = productImages;
                await apiCallInsert.Insert("/Product/Insert", productInsertVm);
                return RedirectToAction("Insert");
            }
            return View(productInsertVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            if (id != 0)
            {
                ProductInsertVm productInsertVm = await apiCallInsert.GetById($"/Product/GetByIdOnlyProduct/{id}");
                if (productInsertVm != null)
                {
                    ViewBag.Catigorys = await apiCallCatigory.GetAll("/Catigory/Get");
                    return View(productInsertVm);
                }
            }
            return Content("Falid To Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductInsertVm productInsertVm)
        {
            if (ModelState.IsValid)
            {
                List<ProductImageInsertVm> productImages = new List<ProductImageInsertVm>();
                ProductInsertVm OldproductInsert = await apiCallInsert.GetById($"/Product/{productInsertVm.Id}");
                await imageRepository.UpdateImageInWroot(OldproductInsert.Image,productInsertVm,productInsertVm.ImageFile);
                for (int i = 0; i < OldproductInsert.ProductImages.Count; i++)
                {
                    ProductImageInsertVm productImageInsertVm=new ProductImageInsertVm() { 
                           Id = OldproductInsert.ProductImages[i].Id,
                           ProductId=OldproductInsert.Id,
                    };
                    await imageRepository.UpdateImageInWroot(OldproductInsert.ProductImages[i].Image, productImageInsertVm, productInsertVm.ProductImagesFiles[i]);
                    productImages.Add(productImageInsertVm);
                }
                productInsertVm.ProductImages = productImages;
                await apiCallInsert.Update("/Product/Edit", productInsertVm);
                return RedirectToAction("Insert");
            }
            return View(productInsertVm);
        }
        public async Task<IActionResult> Delete(int id)
        {

            if (id != 0)
            {
                ProductInsertVm productInsertVm= await apiCallInsert.GetById($"/Product/{id}");
                foreach(ProductImageInsertVm productImageInsertVm in productInsertVm.ProductImages)
                {
                    await imageRepository.DeleteImageInWroot(productImageInsertVm.Image);
                }
                await imageRepository.DeleteImageInWroot(productInsertVm.Image);
                bool state = await apiCallInsert.Delete("/Product/Delete", id.ToString());
                if (state)
                {
                    return RedirectToAction("Insert");
                }
            }
            return Content("Falid To delete");
        }
        public async Task<IActionResult> Details(int id)
        {
            ProductVm productVm = await apiCall.GetById($"/Product/{id}");
            return View(productVm);
        }
    }
}
