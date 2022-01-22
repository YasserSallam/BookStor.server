using System;

namespace DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CoverImageName { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfPages { get; set; }
    }
}
