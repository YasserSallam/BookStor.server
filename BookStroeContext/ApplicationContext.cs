using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace BookStroeContext
{
    public class ApplicationContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired().HasColumnType("NVARCHAR(250)");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Book> Books { get; set; }
    }
}
