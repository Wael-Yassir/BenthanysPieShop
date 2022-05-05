using BenthanysPieShop.Models;
using BenthanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BenthanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this._pieRepository = pieRepository;
            this._categoryRepository = categoryRepository;
        }

        // The view that will be presented will havea the same name as the method by convensions
        // the framework will search for a view under views folder with the same name as the controller
        public ViewResult List()
        {
            PiesListViewModel piesListViewModel = new PiesListViewModel
            {
                Pies = _pieRepository.AllPies,
                CurrentCategory = "Cheese Cake"
            };

            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}
