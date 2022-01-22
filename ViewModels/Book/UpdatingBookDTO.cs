using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Book
{
 public   class UpdatingBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfPages { get; set; }
        public IFormFile Image { get; set; }
    }
}
