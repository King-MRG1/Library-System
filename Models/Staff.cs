using Microsoft.AspNetCore.Identity;

namespace Libarary_System.Models
{
    public class Staff : User
    {
        public int StaffId { get; set; }
        public DateTime HireDate { get; set; }
        public Role StaffRole { get; set; }
        public string NationalNumber { get; set; }
        public List<Borrowing_Transaction> borrowing_Transactions {  get; set; }= new List<Borrowing_Transaction>();
        public enum Role
        {
            Administrative,
            Librarian,
            LibraryTechnician,
            TechnicalSupport,
            Archivist,
            InformationSpecialist
        }
    }
}
