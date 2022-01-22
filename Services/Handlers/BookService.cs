using BookStore.Infrastructure.Services.Contracts;
using BookStroeMapper;
using DTOs.Book;
using Repositories.Contracts;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Services.Handlers
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IFileService _fileService;
        public BookService(IBookRepository bookRepository, IFileService fileService)
        {
            _bookRepository = bookRepository;
            _fileService = fileService;
        }
        public int Create(CreatingBookDTO Creatingbook)
        {
            var coverImageName =string.Empty ;
            if (Creatingbook.Image != null) {
                coverImageName =  _fileService.Save(Creatingbook.Image);
            }
            var dto = BookMapper.Map(Creatingbook);
            dto.CoverImageName = coverImageName;
             var book= BookMapper.Map(dto);
           var id= _bookRepository.Create(book);
            _bookRepository.Commit();
            return id;
        
        }

        public void Delete(int bookId)
        {
            _bookRepository.Delete(bookId);
            _bookRepository.Commit();
        }

        public IEnumerable<BookListingDTO> GetList()
        {
            return BookMapper.Map( _bookRepository.Get().ToList());

        }

        public BookDTO GetById(int bookId)
        {
      var book=  _bookRepository.GetById(bookId);
            return BookMapper.Map(book);
        }

        public void Update(UpdatingBookDTO updatedBook)
        {
            var dbModel = _bookRepository.GetById(updatedBook.Id);
            var coverImageName = string.Empty;
            if (updatedBook.Image != null)
            {
                coverImageName = _fileService.Save(updatedBook.Image);
            }
            else {
                coverImageName = dbModel.CoverImagePath;
            }
             dbModel = BookMapper.MapUpdate(dbModel,updatedBook);
            dbModel.CoverImagePath = coverImageName;
             _bookRepository.Update(dbModel);
            _bookRepository.Commit();
        }
    }
}
