using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;

    }

    public class PeopleModel
    {
        [Key]
        public int peopleId { get; set; }
        public string peopleName { get; set; } = string.Empty;
        public string peopleEmail { get; set; } = string.Empty;
    }

    public class BookTransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public int PeopleId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Borrowed";

        public BookModel Book { get; set; } = null!;
        public PeopleModel People { get; set; } = null!;

    }
}
