namespace Libarary_System.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        List<Book> Books { get; set; } = new List<Book>();
    }
}
