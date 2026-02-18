namespace SuperCchicAPI.Models
{
    public class LoginRequestDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class LoginResponseDTO
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
    }

}
