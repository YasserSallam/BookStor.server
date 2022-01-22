using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Book
{
   public class BookListingDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CoverImageName { get; set; }
        public string AuthorName { get; set; }
    }
}
