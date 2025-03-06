using UnicomTIC_LMS_BE.Entities.Email;
using UnicomTIC_LMS_BE.Models.RequestModel;
using UnicomTIC_LMS_BE.Repositories;

namespace UnicomTIC_LMS_BE.Services
{
    public class SendMailService(SendMailRepository _sendMailRepository, EmailServiceProvider _emailServiceProvider)
    {
        public async Task<string> SendMail(SendMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await EmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.OTP);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Sample System",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "email was sent successfully";
        }

        public async Task<string> EmailBodyGenerate(string emailbody, string? name = null, string? otp = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Otp}", otp }
            };

            foreach (var replacement in replacements)
            {
                if (!string.IsNullOrEmpty(replacement.Value))
                {
                    emailbody = emailbody.Replace(replacement.Key, replacement.Value, StringComparison.OrdinalIgnoreCase);
                }
            }

            return emailbody;
        }
    }
}

