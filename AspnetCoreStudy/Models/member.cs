using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.Models
{
    public class Member
    {
        public int Ix { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<AddressList> AddressList { get; set; }
    }
}
