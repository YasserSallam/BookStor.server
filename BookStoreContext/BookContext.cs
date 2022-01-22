using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace BookStoreContext
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options):base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
