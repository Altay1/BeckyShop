using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeckyShop.Models;
using BeckyShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeckyShop.Controllers
{
    public class PieController : Controller
    {
        // GET: /<controller>/
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
        }

        public ViewResult List()
        {
            PieListViewModel pieList = new PieListViewModel();
            pieList.ListOfPies = _pieRepository.AllPies();
            pieList.CurrentCategory = "Cheese cakes";

            return View(pieList);
        }

        public IActionResult Details(int id)
        {
            var pieById = _pieRepository.PieGetById(id);
            if (pieById == null)
                return NotFound();

            return View(pieById);
        }
    }
}
