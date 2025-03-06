namespace UnicomTIC_LMS_BE.Entities
{
    public class OTP
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public required string Email { get; set; }
        public required string Code { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public User? User { get; set; }
    }
}
