namespace UnicomTIC_LMS_BE.Entities
{
    public class Role
    {
        public Guid ID { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
