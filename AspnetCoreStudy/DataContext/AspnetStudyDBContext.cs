using AspnetCoreStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.DataContext
{
    public class AspnetStudyDBContext : DbContext
    {
        //Migration 과정에 DB를 자동으로 생성해줌
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        //
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            // @기호를 넣으면 뒤의 기호들을 정확히 문자 그대로 전달 하겠다는 뜻
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=AspnetNoteDb;User Id=sa;Password = fbrl0909; ");
        }
    }
}
