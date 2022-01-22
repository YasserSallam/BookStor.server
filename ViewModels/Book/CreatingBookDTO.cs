using Microsoft.AspNetCore.Http;
using System;

namespace DTOs.Book
{
    public class CreatingBookDTO
    {
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfPages { get; set; }
        public IFormFile Image { get; set; }

    }
}
