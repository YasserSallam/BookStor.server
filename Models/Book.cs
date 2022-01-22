using System;

namespace Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CoverImagePath { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfPages { get; set; }
    }
}
