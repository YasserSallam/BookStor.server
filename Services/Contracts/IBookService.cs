using DTOs.Book;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<BookListingDTO> GetList();
        BookDTO GetById(int bookId);
       int Create(CreatingBookDTO book);
        void Update(UpdatingBookDTO book);
        void Delete(int bookId);
    }
}
