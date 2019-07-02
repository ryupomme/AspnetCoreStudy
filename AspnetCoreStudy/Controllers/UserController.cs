using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{
    public class UserController : Controller
    {
        //// GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IHostingEnvironment _hostingEnvironment;
        private string contentRootPath;

        public UserController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            contentRootPath = _hostingEnvironment.ContentRootPath + "/user_list.json";
        }

        public ActionResult List()
        {
            List<Member> items = this.GetDataList();

            ViewData["shipping_info"] = items;

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Member model)
        {
            //validation check
            Member member = new Member();
            AddressList address_info = new AddressList();

            address_info.Name = Request.Form["shipping_name"].ToString();
            address_info.Address = Request.Form["address"].ToString();
            address_info.Zip = Request.Form["zipcode"].ToString();

            member.Age = Int32.Parse(Request.Form["age"].ToString());
            member.Name = Request.Form["name"].ToString();
            member.AddressList = new List<AddressList>();
            member.AddressList.Add(address_info);

            if (ModelState.IsValid)
            {
                List<Member> items = this.AddData(member);
                //file 저장
                this.WriteJsonFile(items);

                return RedirectToAction("List", "User");
            }
            return View(model);
        }

        //public IActionResult Delete()
        public bool Delete([FromBody] JObject jsonData)
        //public string Delete(string aa)
        {
            //return "";
            bool is_sucess = false;
            List<Member> items = this.GetDataList();
            //int delete_item_ix = Int32.Parse(Request.Query["ix"]);

            //Member delete_user = (from c in items
            //                          //where c.Ix == delete_item_ix
            //                      where c.Ix == ix
            //                      orderby c.Ix descending
            //                      select c).FirstOrDefault();

            //if (delete_user != null)
            //{
            //    items.Remove(delete_user);
            //    this.WriteJsonFile(items);
            //    is_sucess = true;
            //}

            //return RedirectToAction("List", "User");
            return is_sucess;
        }

        private List<Member> AddData(Member newUser)
        {
            List<Member> members = this.GetDataList();

            int lastIx = (from member in members
                           orderby member.Ix descending
                           select member.Ix).FirstOrDefault();

            newUser.Ix = lastIx + 1;
            members.Add(newUser);

            return members;
        }

        /// <summary>
        /// Get Data from Json File
        /// </summary>
        /// <returns></returns>
        private List<Member> GetDataList()
        {
            StreamReader r = new StreamReader(this.contentRootPath);
            string json = r.ReadToEnd();
            r.Close();

            List<Member> items = JsonConvert.DeserializeObject<List<Member>>(json);

            return items;
        }

        /// <summary>
        /// json file로 데이터 저장
        /// </summary>
        /// <param name="items"></param>
        private void WriteJsonFile(List<Member> items)
        {
            string sJSONResponse = JsonConvert.SerializeObject(items);
            System.IO.File.WriteAllText(this.contentRootPath, sJSONResponse);
        }
    }
}
