namespace UnicomTIC_LMS_BE.Entities.Email
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public EmailType EmailType { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public enum EmailType
    {
        None = 0,
        OTP = 1,
        Deactive = 2,

    }
}
