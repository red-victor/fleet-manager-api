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
        public string Cnp { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ImgSrc { get; set; }
        public string ImgName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
