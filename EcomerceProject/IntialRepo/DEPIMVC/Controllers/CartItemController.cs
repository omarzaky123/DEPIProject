using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DEPIMVC.Controllers
{
    public class CartItemController : Controller
    {
        private readonly IApiCall<CartItemInsert> apiCall;

        public CartItemController(IApiCall<CartItemInsert> apiCall)
        {
            this.apiCall = apiCall;
        }
        public async Task<IActionResult> Delete(int id)
        {
            bool state = await apiCall.Delete("/CartItem/Delete", id.ToString());
            if (state) { 
            return RedirectToAction("ShowShopping","ShoppingCart");
            }
            return Content("There Is An Error Happen");
        }
    }
}
