using AspnetCoreStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.ViewModel
{
    public class MypageVM
    {
        public Member member { get; set; }
        //public List<RankGroup> rankGroupList { get; set; }
        public List<Product> productList { get; set; }
        public List<Order> orderedList { get; set; }
        public List<WishBrand> wishBrandList { get; set; }
        public List<WishProduct> wishProductList { get; set; }
        public List<Plan> planList { get; set; }
    }
}
