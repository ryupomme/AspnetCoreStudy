﻿// <auto-generated />
using AspnetCoreStudy.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspnetCoreStudy.Migrations
{
    [DbContext(typeof(AspnetStudyDBContext))]
    [Migration("20190701010415_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspnetCoreStudy.Models.Note", b =>
                {
                    b.Property<int>("No")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contents")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserNo");

                    b.HasKey("No");

                    b.HasIndex("UserNo")
                        .IsUnique();

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("AspnetCoreStudy.Models.User", b =>
                {
                    b.Property<int>("No")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Id")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Password");

                    b.HasKey("No");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AspnetCoreStudy.Models.Note", b =>
                {
                    b.HasOne("AspnetCoreStudy.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("AspnetCoreStudy.Models.Note", "UserNo")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
