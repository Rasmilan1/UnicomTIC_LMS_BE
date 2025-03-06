namespace UnicomTIC_LMS_BE.Entities.Email
{
    public class EmailConfig
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
