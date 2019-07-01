using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        // allocation이 없으면 default로 HttpGet 이다
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            //validation check

            if (ModelState.IsValid)
            {
                //db입출력할때 입력서 open connection, close connection. 매번 작성하지 않고 using문 사용
                // Java -> try(SqlSession){} catch{}
                using(var db = new AspnetStudyDBContext())
                {
                    db.Users.Add(model);        // memory에
                    db.SaveChanges();           // DB에 반영
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
