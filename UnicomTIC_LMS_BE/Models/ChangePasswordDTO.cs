namespace UnicomTIC_LMS_BE.Models
{
    public class ChangePasswordDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
