using System.Text.Json.Serialization;

namespace Libarary_System.Models
{
    public class Author:User
    {
        public int AuthorId { get; set; }
        public string Biography { get; set; }   
        public Nationalit Nationality { get; set; }
        public enum Nationalit
        {
            American,
            British,
            Canadian,
            Chinese,
            French,
            German,
            Indian,
            Italian,
            Japanese,
            Russian,
            Spanish,
            Egyptian,
            other
        }
        [JsonIgnore]
        public List<Book_Author> book_Authors { get; set; }
    }
}
