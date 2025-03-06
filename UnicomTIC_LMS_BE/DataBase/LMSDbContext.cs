using Microsoft.EntityFrameworkCore;
using UnicomTIC_LMS_BE.Entities;
using UnicomTIC_LMS_BE.Entities.Email;

namespace UnicomTIC_LMS_BE.DataBase
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

       
    }
}
