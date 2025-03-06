using System.ComponentModel.DataAnnotations;

namespace UnicomTIC_LMS_BE.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public UserRole UserRole { get; set; }

        //public ICollection<Student> Students { get; set; }
        //public ICollection<Staff> Staffs { get; set; }
        //public ICollection<Teacher> Teachers { get; set; }
    }
}
