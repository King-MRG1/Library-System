namespace Libarary_System.Models
{
    public class Publisher : User
    {
        public int PublisherId { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

    }
}
