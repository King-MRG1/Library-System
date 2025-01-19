using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Libarary_System.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        [JsonIgnore]
        public List<Book_Author> Author { get; set; } = new List<Book_Author>();
        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }
        [JsonIgnore]
        public Publisher Publisher { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        [JsonIgnore]
        public List<Borrowing_Transaction> BorrowingTransactions { get; set; }= new List<Borrowing_Transaction>();
    }
}
