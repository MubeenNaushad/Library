using BookStore.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookStore.Data
{
    public class DbInitialization
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if(context.Books.Any()) return;

            var books = new BookModel[]
            {
                new BookModel{ Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10 },
                new BookModel{ Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 7 },
                new BookModel{ Title = "1984", Author = "George Orwell", Price = 8 },
                new BookModel{ Title = "Pride and Prejudice", Author = "Jane Austen", Price = 6 },
                new BookModel{ Title = "The Catcher in the Rye", Author = "J.D. Salinger", Price = 9 }
            };

            var peoples = new PeopleModel[]
            {
                new PeopleModel{ peopleName = "John Doe", peopleEmail = "abc@gmail.com"},
                new PeopleModel{ peopleName = "Jane Smith", peopleEmail = "def@gmail.com" },

            };

            context.Books.AddRange(books);
            context.SaveChanges();

        }
    }
}
