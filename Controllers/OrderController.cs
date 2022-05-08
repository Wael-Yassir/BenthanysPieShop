using System.Linq;
using BenthanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenthanysPieShop.Controllers
{
    public class OrderController : Controller
    {
        public IOrderRepository _orderRepository { get; set; }
        public ShoppingCart _shoppingCart { get; set; }

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            this._orderRepository = orderRepository;
            this._shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first!");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order, You will soon enjoy our delicious pies!";
            
            return View();
        }
    }
}
