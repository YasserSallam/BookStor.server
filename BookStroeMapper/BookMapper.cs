using DTOs.Book;
using Models;
using System.Collections.Generic;

namespace BookStroeMapper
{
    public static class BookMapper
    {
        public static Book Map(BookDTO DTO) {
            return new Book()
            {
                Id = DTO.Id,
                AuthorName = DTO.AuthorName,
                CoverImagePath = DTO.CoverImageName,
                NumberOfPages = DTO.NumberOfPages,
                PublishedDate = DTO.PublishedDate,
                Title = DTO.Title
            };
        }
        public static BookDTO Map(Book DTO)
        {
            return new BookDTO()
            {
                Id = DTO.Id,
                AuthorName = DTO.AuthorName,
                CoverImageName = DTO.CoverImagePath,
                NumberOfPages = DTO.NumberOfPages,
                PublishedDate = DTO.PublishedDate,
                Title = DTO.Title
            };
        }
        public static BookDTO Map(CreatingBookDTO DTO)
        {
            return new BookDTO()
            {
                AuthorName = DTO.AuthorName,
                NumberOfPages = DTO.NumberOfPages,
                PublishedDate = DTO.PublishedDate,
                Title = DTO.Title
            };
        }

        #region Map Listing
        public static BookListingDTO MapToListDTO(Book book)
        {
            return new BookListingDTO()
            {
                Id = book.Id,
                AuthorName = book.AuthorName,
                PublishedDate = book.PublishedDate,
                Title = book.Title,
                CoverImageName = book.CoverImagePath,
            };
        }

        public static IEnumerable<BookListingDTO> Map(List<Book> books)
        {
            var result = new List<BookListingDTO>();
            foreach (var book in books)
            {
                result.Add(MapToListDTO(book));
            }
            return result;
        }

        public static IEnumerable<Book> Map(List<BookDTO> books)
        {
            var result = new List<Book>();
            foreach (var book in books)
            {
                result.Add(Map(book));
            }
            return result;
        }
        #endregion

        #region Map update
        public static Book MapUpdate(Book book,UpdatingBookDTO updated) {
            book.AuthorName = updated.AuthorName;
            book.NumberOfPages = updated.NumberOfPages;
            book.PublishedDate = updated.PublishedDate;
            book.Title = updated.Title;
            return book;
        }
        #endregion
    }
}
