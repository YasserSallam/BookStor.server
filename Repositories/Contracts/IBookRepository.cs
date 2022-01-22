using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Contracts
{
   public interface IBookRepository
    {
        IEnumerable<Book> Get();
        Book GetById(int bookId);
        int Create(Book book);
        void Update(Book book);
        void Delete(int bookId);
        void Commit();
    }
}
