using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.Models
{
    public class User
    {
        [Key]       // PK // user number
        public int No { get; set; }

        [Required(ErrorMessage = "사용자 이름을 입력하세요.")]
        // user name
        public string Name { get; set; }

        [Required(ErrorMessage = "사용자 아이디을 입력하세요.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        public int Password { get; set; }
    }
}
