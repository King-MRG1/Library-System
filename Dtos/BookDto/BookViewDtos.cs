namespace Libarary_System.Dtos.BookDto
{
    public class BookViewDtos
    {
        public int Id { get; set; }
        public string Tilte { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
        public List<string> AuthorsName { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
    }
}
