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
    }
}
