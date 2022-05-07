using Microsoft.AspNetCore.Mvc;

namespace BenthanysPieShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
