namespace EmployeeManager.DTOs
{
    public class LoggedUserDto : UserDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
