namespace ECommerce.API.Models.DTOs
{
    public class UserDTO
    {
        public UserDTO(string userName, string email, string role)
        {
            UserName = userName;
            Email = email;
            Role = role;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
