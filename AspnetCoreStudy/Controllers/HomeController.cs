using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreStudy.Models;

namespace AspnetCoreStudy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user1 = new User();
            user1.No = 1;
            user1.Name = "gihoon";

            var user2 = new User
            {
                No = 1,
                Name = "gihoon"
            };

            // 1번째
            //return View(user1);

            // 2 ViewBag
            //ViewBag.User = user1;

            // 3 ViewData mvc6에 처음 나옴
            ViewData["No"] = user1.No;
            ViewData["Name"] = user1.Name;

            return View(user1);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
