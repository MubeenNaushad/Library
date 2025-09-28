namespace BookStore.Models
{
    public class CreateOrUpdate
    {
        public string? bookTitle { get; set; }
        public string? authorName { get; set; }
        public decimal? priceValue { get; set; } = 0;
    }


}
