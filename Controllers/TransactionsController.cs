using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly DataContext _library;

        public TransactionsController(DataContext library)
        {
            _library = library;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BookReq books)
        {
            if (books == null || books.peopleId <= 0 || books.bookId <= 0) return BadRequest("Cannot be empty.");

            var book = await _library.Books.FindAsync(books.bookId);

            var person = await _library.Peoples.FindAsync(books.peopleId);

            if (book == null || person == null) return BadRequest("Book or Person not found.");

            var transaction = new BookTransactionModel
            {
                BookId = books.bookId,
                PeopleId = books.peopleId,
                BorrowDate = DateTime.Now,
                Status = "Borrowed"
            };

            _library.BookTransactions.Add(transaction);
            await _library.SaveChangesAsync();

            return Ok(new { message = "Book borrowed successfully.", transaction });
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] BookReq books)
        {
            if (books == null || books.peopleId <= 0 || books.bookId <= 0) return BadRequest("Cannot be empty.");

            var transaction = _library.BookTransactions
                .FirstOrDefault(t => t.BookId == books.bookId && t.PeopleId == books.peopleId && t.Status == "Borrowed");

            if (transaction == null) return BadRequest("No active borrow transaction found for this book and person.");

            transaction.ReturnDate = DateTime.Now;
            transaction.Status = "Returned";
            await _library.SaveChangesAsync();
            return Ok(new { message = "Book returned successfully.", transaction });
        }

        [HttpGet("borrowed")]
        public IActionResult GetAllBorrowedBooks()
        {
            var borrowedBooks = _library.BookTransactions
                .Where(t => t.Status == "Borrowed")
                .ToList();
            return Ok(borrowedBooks);

        }

        [HttpGet("personHistory")]
        public IActionResult GetPersonTransactionHistory(int personId)
        {
            if (personId <= 0) return BadRequest("Invalid person ID.");

            var history = _library.BookTransactions
                .Where(t => t.PeopleId == personId).OrderByDescending(t => t.BorrowDate)
                .ToList();

            return Ok(history);
        }

        public class BookReq
        {
            public int bookId { get; set; }
            public int peopleId { get; set; }
        }
    }
}
