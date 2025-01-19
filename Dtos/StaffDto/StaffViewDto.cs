namespace Libarary_System.Dtos.StaffDto
{
    public class StaffViewDto
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }
        public List<string> Borrowing { get; set; } = new List<string>(); 
    }
}
