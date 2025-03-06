using UnicomTIC_LMS_BE.Entities.Email;

namespace UnicomTIC_LMS_BE.Models.RequestModel
{
    public class SendMailRequest
    {
        public string? Name { get; set; }
        public string? OTP { get; set; }
        public string? Email { get; set; }
        public EmailType EmailType { get; set; }
    }
}
