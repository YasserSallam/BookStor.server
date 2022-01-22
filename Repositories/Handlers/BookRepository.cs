using BookStroeContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Repositories.Handlers
{
    public class BookRepository : IBookRepository
    {
        private ApplicationContext _context;
        DbSet<Book> _booksTable;
        public BookRepository(ApplicationContext context)
        {
            _context = context;
            _booksTable = _context.Set<Book>();
            
        }
        public int Create(Book book)
        {
            _booksTable.Add(book);
            return book.Id;
        }

        public void Delete(int bookId)
        {
            var book = _booksTable.Find(bookId);
            _booksTable.Remove(book);
        }

        public IEnumerable<Book> Get()
        {
            return _booksTable.ToList();
        }

        public Book GetById(int bookId)
        {
            return _booksTable.Find(bookId);
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }
        public void Commit() {
            _context.SaveChanges();
        }
    }
}
