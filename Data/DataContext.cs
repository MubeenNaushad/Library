using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<PeopleModel> Peoples { get; set; }
        public DbSet<BookTransactionModel> BookTransactions { get; set; }


    }
}
