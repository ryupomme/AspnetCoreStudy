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

        #region UserInfo

        public ActionResult List()
        {
            List<Member> members = this.GetDataList();

            ViewData["shipping_info"] = members;

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public IActionResult AddUserInfo(Member model)
        {
            //validation check
            Member member = new Member();

            member.Age = Int32.Parse(Request.Form["age"].ToString());
            member.Name = Request.Form["name"].ToString();
            member.AddressList = new List<AddressList>();

            List<Member> members = this.AddData(member);
            //file 저장
            this.WriteJsonFile(members);

            return RedirectToAction("List", "User");
        }

        public ActionResult Edit()
        {
            int ix = Int32.Parse(Request.Query["ix"]);
            Member member = this.GetData(ix);
            ViewData["userInfo"] = member;

            return View();
        }

        public IActionResult EditUserInfo()
        {
            int user_ix = Int32.Parse(Request.Form["user_ix"]);

            List<Member> members = this.GetDataList();

            Member selectedMember = (from member in members
                                     where member.Ix == user_ix
                                     select member).FirstOrDefault();

            if (selectedMember != null)
            {
                selectedMember.Name = Request.Form["name"].ToString();
                selectedMember.Age = Int32.Parse(Request.Form["age"]);
            }

            WriteJsonFile(members);

            return RedirectToAction("List", "User");
        }

        public IActionResult Detail()
        {
            int detailMemberIx = Int32.Parse(Request.Query["ix"]);

            List<Member> members = this.GetDataList();

            Member detailMember = (from member in members
                                   where member.Ix == detailMemberIx
                                   select member).FirstOrDefault();

            ViewData["userInfo"] = detailMember;

            return View();
        }

        //public IActionResult Delete()
        public bool DeleteUserInfo([FromBody] JObject jsonData)
        {
            //return "";
            bool is_sucess = false;
            List<Member> members = this.GetDataList();
            //int delete_item_ix = Int32.Parse(Request.Query["ix"]);
            //int delete_item_ix = Int32.Parse(jsonData["ix"].ToString());
            int[] deleteIxArray = Array.ConvertAll(jsonData["ix"].ToArray(), s => Int32.Parse(s.ToString()));
            Member[] deleteUsers = (from c in members
                                        //where c.Ix == delete_item_ix
                                        //where jsonData["ix"].ToArray().Contains(c.Ix.ToString())
                                        //where c.Ix == ix
                                    where deleteIxArray.Contains(c.Ix)
                                    orderby c.Ix descending
                                    select c).ToArray();

            if (deleteUsers != null && deleteUsers.Length > 0)
            {
                foreach(Member deleteUser in deleteUsers)
                {
                    members.Remove(deleteUser);
                }
                this.WriteJsonFile(members);
                is_sucess = true;
            }

            return is_sucess;
        }

        #endregion

        #region AddressInfo

        public ActionResult AddAddress()
        {
            int ix = Int32.Parse(Request.Query["ix"]);
            Member member = this.GetData(ix);
            ViewData["userInfo"] = member;
            return View();
        }

        public IActionResult AddAddressInfo()
        {
            int userIx = Int32.Parse(Request.Form["user_ix"]);
            AddressList address_info = new AddressList();
            address_info.Name = Request.Form["shipping_name"].ToString();
            address_info.Address = Request.Form["address"].ToString();
            address_info.Zip = Request.Form["zipcode"].ToString();

            List<Member> members = this.GetDataList();

            Member selectedMember = (from member in members
                                     where member.Ix == Int32.Parse(Request.Form["user_ix"])
                                     select member).FirstOrDefault();
            selectedMember.AddressList.Add(address_info);

            WriteJsonFile(members);

            return RedirectToAction("Detail", "User", new { ix = userIx });
        }

        public bool DeleteAddressInfo([FromBody] JObject jsonData)
        {
            bool is_sucess = false;
            List<Member> members = this.GetDataList();
            int userIx = Int32.Parse(jsonData["ix"].ToString());
            string[] deleteNameArray = Array.ConvertAll(jsonData["name"].ToArray(), s => s.ToString());

            Member deleteUser = (from c in members
                                      //where c.Ix == delete_item_ix
                                      //where jsonData["ix"].ToArray().Contains(c.Ix.ToString())
                                      //where c.Ix == ix
                                  where c.Ix == userIx
                                  select c).FirstOrDefault();

            AddressList[] deleteAddressArray = (from addr in deleteUser.AddressList
                                                where deleteNameArray.Contains(addr.Name)
                                                select addr).ToArray();

            if (deleteAddressArray != null && deleteAddressArray.Length > 0)
            {
                foreach (AddressList deleteAddress in deleteAddressArray)
                {
                    deleteUser.AddressList.Remove(deleteAddress);
                }
                this.WriteJsonFile(members);
                is_sucess = true;
            }

            return is_sucess;
        }

        #endregion

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

            List<Member> members = JsonConvert.DeserializeObject<List<Member>>(json);

            return members;
        }

        /// <summary>
        /// Get Data from Json File
        /// </summary>
        /// <returns></returns>
        private Member GetData(int ix)
        {
            StreamReader r = new StreamReader(this.contentRootPath);
            string json = r.ReadToEnd();
            r.Close();

            List<Member> members = JsonConvert.DeserializeObject<List<Member>>(json);

            Member data = (from member in members
                           where member.Ix == ix
                           select member).FirstOrDefault();

            return data;
        }

        /// <summary>
        /// json file로 데이터 저장
        /// </summary>
        /// <param name="members"></param>
        private void WriteJsonFile(List<Member> members)
        {
            string sJSONResponse = JsonConvert.SerializeObject(members);
            System.IO.File.WriteAllText(this.contentRootPath, sJSONResponse);
        }
    }
}
