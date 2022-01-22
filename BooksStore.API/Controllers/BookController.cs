using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Collections.Generic;
using ViewModels;

namespace BooksStore.API.Controllers
{
    public class BookController : ControllerBase
    {
       private IBookService _bookService;
        public BookController(IBookService bookService)
        {

        }
        public IEnumerable<BookDTO> Index()
        {
            return View();
        }
    }
}
