using DEPIMVC.Models;
using DEPIMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DEPIMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IApiCall<GusetVm> apiCallGuset;
        private readonly IApiCall<ShoppingCartVm> apiCallShoopingCart;
        private readonly IApiCall<CartItem> apiCallCartItem;
        private readonly IApiCall<OrderVm> apiCallOrder;
        private readonly IApiCall<OrderItemVm> apiCallOrderItem;
        private readonly IApiCall<OrderWithRelatedOrderItemsVm> apiCallOrderWithRelated;
        private readonly IApiCall<ProductVationInsertVm> apiCallProductVationInsertVm;
        private readonly IApiCall<OrderFullModelVm> apiCallOrderFullMode;

        public OrderController(IApiCall<GusetVm> apiCallGuset
            ,IApiCall<ShoppingCartVm> apiCallShoopingCart
            ,IApiCall<CartItem> apiCallCartItem
            ,IApiCall<OrderVm> apiCallOrder,
            IApiCall<OrderItemVm> apiCallOrderItem
            ,IApiCall<OrderWithRelatedOrderItemsVm> apiCallOrderWithRelated,
            IApiCall<ProductVationInsertVm> apiCallProductVationInsertVm,IApiCall<OrderFullModelVm> apiCallOrderFullMode)
        {
            this.apiCallGuset = apiCallGuset;
            this.apiCallShoopingCart = apiCallShoopingCart;
            this.apiCallCartItem = apiCallCartItem;
            this.apiCallOrder = apiCallOrder;
            this.apiCallOrderItem = apiCallOrderItem;
            this.apiCallOrderWithRelated = apiCallOrderWithRelated;
            this.apiCallProductVationInsertVm = apiCallProductVationInsertVm;
            this.apiCallOrderFullMode = apiCallOrderFullMode;
        }
        [Authorize]
        public async Task<IActionResult> MangeOrder([FromQuery]int TotalAmount)
        {
            string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            GusetVm gusetVm = await apiCallGuset.GetById($"/Guset/ByUser/{userid}");
            ShoppingCartVm shoppingCartVm = await apiCallShoopingCart.GetById($"/ShoppingCart/GusetId/{gusetVm.Id}");
            if (shoppingCartVm != null)
            {
                OrderVm order = new OrderVm()
                {
                    GusetId = gusetVm.Id,
                    Status = "Wating",
                    TotalAmount=TotalAmount, 
                };

                order = await apiCallOrder.Insert($"/Order/Insert/",order);
                var cartItems = await apiCallCartItem.GetAll($"/ShoppingCart/RelatedCard/{shoppingCartVm?.Id ?? 0}");
                foreach (var cartItem in cartItems) {
                    OrderItemVm OrderItem =new OrderItemVm() { 
                        Quantity = cartItem.Quantity,
                        ProductVartionId=cartItem.ProductVartionID,
                        OrderID=order.Id,
                    };
                    ProductVationInsertVm productVationInsertVm = await apiCallProductVationInsertVm
                        .GetById($"/ProductVartion/GetById/{cartItem.ProductVartionID}");
                    productVationInsertVm.Quantity_In_Stock -=cartItem.Quantity;
                    await apiCallProductVationInsertVm.Update("/ProductVartion/Edit",productVationInsertVm);
                    await apiCallOrderItem.Insert("/OrderItem/Insert",OrderItem);
                }

                //delete the shopping cart
             bool state = await apiCallShoopingCart.Delete("/ShoppingCart/Delete", shoppingCartVm.Id.ToString());
            }
            return RedirectToAction("ShowOrder");
        }

        public async Task<IActionResult> ShowOrder()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new
                {
                    Url = $"/Order/ShowOrder"
                });
            }
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            GusetVm guestVm = await apiCallGuset.GetById($"/Guset/ByUser/{userId}");
            List<OrderWithRelatedOrderItemsVm> orders = await apiCallOrderWithRelated.GetAll($"/Order/GetAllByGuset/{guestVm.Id}");
            ViewBag.Address = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress")?.Value;
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
                List<OrderFullModelVm> orderVms = await apiCallOrderFullMode.GetAll("/Order/GetAll");
                return View(orderVms);
            
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id != 0)
            {
                OrderFullModelVm orderFullModelVms = await apiCallOrderFullMode.GetById($"/Order/GetByIdWithItems/{id}");
                return View(orderFullModelVms);
            }
            return BadRequest("The Id Is Invalid");
        }


        [Authorize]
        public async Task<IActionResult> DeleteGuset(int id,string Return= "ShowOrder")
        {
            if (id != 0)
            {
                OrderFullModelVm orderFullModelVms = await apiCallOrderFullMode.GetById($"/Order/GetByIdWithItems/{id}");
                foreach (OrderItemFullModel orderItem in orderFullModelVms.OrderItems)
                {
                    ProductVationInsertVm productVationInsertVm = await apiCallProductVationInsertVm
                                .GetById($"/ProductVartion/GetById/{orderItem.ProductVartionId}");
                    productVationInsertVm.Quantity_In_Stock += orderItem.Quantity;
                    await apiCallProductVationInsertVm.Update("/ProductVartion/Edit", productVationInsertVm);
                }
                bool state = await apiCallOrder.Delete("/Order/Delete", id.ToString());
            }
            return RedirectToAction(Return);
        }

        public async Task<IActionResult> Edit(int id)
        {

            if (id != 0)
            {
                OrderVm orderInsert = await apiCallOrder.GetById($"/Order/{id}");
                if (orderInsert != null)
                {
                    return View(orderInsert);
                }
            }
            return Content("Falid To Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderVm orderVm)
        {
            if (ModelState.IsValid)
            {
                await apiCallOrder.Update("/Order/Edit", orderVm);
                return RedirectToAction("GetAll");
            }
            return View(orderVm);
        }



    }
}
