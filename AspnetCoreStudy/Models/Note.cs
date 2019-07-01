using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.Models
{
    public class Note
    {
        [Key]
        public int No { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Contents { get; set; }
        [Required]
        public int UserNo { get; set; }

        // virtual은 없어도 정상 작동 함. lazy loading
        [ForeignKey("UserNo")]
        public virtual User User { get; set; }

    }
}
