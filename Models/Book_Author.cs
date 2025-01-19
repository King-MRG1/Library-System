using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Libarary_System.Models
{
    public class Book_Author
    {
        public int Book_AuthorId { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }
    }
}
