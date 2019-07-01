using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{
    public class studyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //DataSet ds = new DataSet();
            //DataRow dr = new DataRow();
            return View();
        }

        public IActionResult Java()
        {
            return View();
        }

        public IActionResult CSharp()
        {
            return View();
        }

        public IActionResult Cpp()
        {
            return View();
        }
    }
}
