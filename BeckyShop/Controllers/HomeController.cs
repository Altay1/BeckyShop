using BeckyShop.Models;
using BeckyShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeckyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pirRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pirRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                PiesOfTheWeek = _pirRepository.PiesOfTheWeek()
            };

            return View(homeViewModel);
        }
    }

}

