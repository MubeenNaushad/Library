using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly DataContext _data;

        public BookController(ILogger<BookController> logger, DataContext data)
        {
            _logger = logger;
            _data = data;
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] CreateOrUpdate creation)
        {
            if(creation.bookTitle == null || creation.authorName == null|| creation.priceValue == null || creation.priceValue <= 0) 
                return BadRequest("Please provide valid book details.");
            var book = new BookModel { Title = creation.bookTitle, Author = creation.authorName, Price = creation.priceValue.Value };

            _data.Books.Add(book);
            _data.SaveChanges();

            return Ok(book);
        }

        [HttpGet("getAll")]
        public IActionResult GetAllBooks()
        {
            var books = _data.Books.ToList();

            return Ok(books);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetBooksbyId(int id)
        {
            var book = _data.Books.Find(id);
            if(book == null) return BadRequest("Book not found.");
            
            return Ok(book);
        }

        [HttpPatch("update/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] CreateOrUpdate updation)
        {
            if (id <= 0) return BadRequest("Invalid book ID.");

            var book = _data.Books.Find(id);
            if (book == null) return BadRequest("Book not found.");

            if(!string.IsNullOrEmpty(updation.bookTitle)) book.Title = updation.bookTitle;
            if(!string.IsNullOrEmpty(updation.authorName)) book.Author = updation.authorName;
            if(updation.priceValue > 0) book.Price = updation.priceValue.Value;

            _data.SaveChanges();

            return Ok(book);

        }

    }

}
