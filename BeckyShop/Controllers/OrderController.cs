using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeckyShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeckyShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart shoppingCart;

        public OrderController(IOrderRepository orderRepository,
            ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }
        // GET: /<controller>/
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = shoppingCart.GetShoppingCartItems();

            shoppingCart.ShoppingCartItems = cartItems;

            if(cartItems.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty!");
            }

            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank for your order";

            return View();
        }
    }
}
