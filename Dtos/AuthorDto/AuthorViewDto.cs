namespace Libarary_System.Dtos.AuthorDto
{
    public class AuthorViewDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Emaill { get; set; }
        public string Biography { get; set; }
        public string Nationality { get; set; }
        public List<string> Books { get; set; } = new List<string>();
    }
}
