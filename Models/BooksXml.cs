using System.Xml.Serialization;

namespace BookStore.Models
{
    [XmlRoot("Books")]
    public class BooksXml
    {
        [XmlElement("Book")]
        public List<BooksRecord> XmlBooks { get; set; } = new();
    }

    public class BooksRecord
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
