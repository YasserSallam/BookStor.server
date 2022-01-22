using DTOs.Book;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookListingDTO>> GetList()
        {
            return Ok(_bookService.GetList());
        }

        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetById(int id)
        {

            return Ok( _bookService.GetById(id));
        }

        [HttpPost]
        public ActionResult<int> CreateBook([FromForm] CreatingBookDTO bookDTO)
        {
            return Ok(_bookService.Create(bookDTO));
        }
        [HttpPost]
        public ActionResult UpdateBook([FromForm] UpdatingBookDTO bookDTO)
        {

             _bookService.Update(bookDTO);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult DeleteBook(int id)
        {

            _bookService.Delete(id);
            return Ok();

        }
    }
}
