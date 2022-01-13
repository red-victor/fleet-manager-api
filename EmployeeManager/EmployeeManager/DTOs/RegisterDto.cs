using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CNP { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
    }
}
