using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace DEPIMVC.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IApiCall<ShoppingCartVm> apiCall;
        private readonly IApiCall<CartItem> apiCallCartItem;
        private readonly IApiCall<CartItemInsert> apiCallCartItemInsert;
        private readonly IApiCall<GusetVm> apiCallGuset;
        private readonly IApiCall<ProductVationInsertVm> apiCallProductVarInsertVm;

        public ShoppingCartController(IApiCall<ShoppingCartVm> apiCall
            ,IApiCall<CartItem> apiCallCartItem,
            IApiCall<CartItemInsert> apiCallCartItemInsert,IApiCall<GusetVm> apiCallGuset,IApiCall<ProductVationInsertVm> apiCallProductVarInsertVm)
        {
            this.apiCall = apiCall;
            this.apiCallCartItem = apiCallCartItem;
            this.apiCallCartItemInsert = apiCallCartItemInsert;
            this.apiCallGuset = apiCallGuset;
            this.apiCallProductVarInsertVm = apiCallProductVarInsertVm;
        }


        #region Old
        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> MangeShopping(int ProductVartionID
        //    ,int Quantity)
        //{

        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Account", new
        //        {
        //            Url = $"/ShoppingCart/MangeShopping?ProductVartionID={ProductVartionID}&Quantity={Quantity}"
        //        });
        //    }

        //    //im Known--> i have cart or i do not have
        //    List<CartItem> cartItemsOut=new List<CartItem>();
        //    CartItemInsert cartItem = new CartItemInsert();
        //    cartItem.ProductVartionID = ProductVartionID;
        //    cartItem.Quantity = Quantity;

        //    string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //    GusetVm gusetVm= await apiCallGuset.GetById($"/Guset/ByUser/{userid}");
        //    ShoppingCartVm shoppingCartVm = await apiCall.GetById($"/ShoppingCart/GusetId/{gusetVm.Id}");
        //    if (shoppingCartVm != null)
        //    {
        //        //Here I Have A shoppingCartVm
        //        cartItem.ShoppingCartId = shoppingCartVm.Id;
        //        await apiCallCartItemInsert.Insert("/CartItem/Insert", cartItem);
        //        cartItemsOut = await apiCallCartItem.GetAll($"/ShoppingCart/RelatedCard/{shoppingCartVm.Id}");
        //    }
        //    else
        //    {
        //        //Here I Do not Have shoppingCartVm
        //        #region Create New Shooping
        //        ShoppingCartVm shopping = new ShoppingCartVm();
        //        shopping.GusetId = gusetVm.Id;
        //        var response = await apiCall.Insert("/ShoppingCart/Insert", shopping);
        //        #endregion

        //        cartItem.ShoppingCartId = response.Id;
        //        await apiCallCartItemInsert.Insert("/CartItem/Insert", cartItem);
        //        cartItemsOut = await apiCallCartItem.GetAll($"/ShoppingCart/RelatedCard/{response.Id}");
        //    }
        //    return View(cartItemsOut);
        //}

        #endregion

        [HttpGet]
        public async Task<IActionResult> MangeShopping(int ProductVartionID, int Quantity)
        {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account", new
                    {
                        Url = $"/ShoppingCart/MangeShopping?ProductVartionID={ProductVartionID}&Quantity={Quantity}"
                    });
                }
                List<CartItem> cartItemsOut = new List<CartItem>();
                CartItemInsert cartItem = new CartItemInsert
                {
                    ProductVartionID = ProductVartionID,
                    Quantity = Quantity
                };

                string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                GusetVm gusetVm = await apiCallGuset.GetById($"/Guset/ByUser/{userid}");
                ShoppingCartVm shoppingCartVm = await apiCall.GetById($"/ShoppingCart/GusetId/{gusetVm.Id}");

                if (shoppingCartVm != null)
                {
                    // Existing cart - add item
                    cartItem.ShoppingCartId = shoppingCartVm.Id;
                    await apiCallCartItemInsert.Insert("/CartItem/Insert", cartItem);
                }
                else
                {
                    // New cart - create and add item
                    ShoppingCartVm shopping = new ShoppingCartVm { GusetId = gusetVm.Id };
                    var response = await apiCall.Insert("/ShoppingCart/Insert", shopping);
                    cartItem.ShoppingCartId = response.Id;
                    await apiCallCartItemInsert.Insert("/CartItem/Insert", cartItem);
                }

                return RedirectToAction("ShowShopping",new { ProductVartionID = ProductVartionID });
        }

        
        public async Task<IActionResult> ShowShopping()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new
                {
                    Url = $"/ShoppingCart/ShowShopping"
                });
            }

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            GusetVm guestVm = await apiCallGuset.GetById($"/Guset/ByUser/{userId}");
            ShoppingCartVm cartVm = await apiCall.GetById($"/ShoppingCart/GusetId/{guestVm.Id}");
            var cartItems = await apiCallCartItem.GetAll($"/ShoppingCart/RelatedCard/{cartVm?.Id ?? 0}");

            //foreach (var cartItem in cartItems) {
            //    ProductVationInsertVm productInsertVm = await apiCallProductVarInsertVm.GetById($"/ProductVartion/GetById/{cartItem.ProductVartionID}");


            //}


            return View(cartItems);

        }

    }
}


