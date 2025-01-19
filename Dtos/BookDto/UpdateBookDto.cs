using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Dtos.BookDto
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "The ISBN is Required")]
        [MinLength(6, ErrorMessage = "The ISBN should not be less than 6 charcaters"), MaxLength(6, ErrorMessage = "The ISBN should not be more than 6 charcaters")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "The Title is Required")]
        [MaxLength(100, ErrorMessage = "The Title should not be more than 100 charcaters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The Publication Year is Required")]
        public int PublicationYear { get; set; }
        [Required(ErrorMessage = "The Totla Copies is Required")]
        public int TotalCopies { get; set; }
        [Required(ErrorMessage = "The Publisher Id is Required")]
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "The Category Id is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The Authors Id is Required")]
        public List<int> AuthorIds { get; set; }
    }
}
