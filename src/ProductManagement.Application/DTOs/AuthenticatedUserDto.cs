namespace ProductManagement.Application.DTOs
{
    public class AuthenticatedUserDto
    {
        public string Id { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
