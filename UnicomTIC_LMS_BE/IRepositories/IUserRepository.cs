using UnicomTIC_LMS_BE.Entities;
using UnicomTIC_LMS_BE.Models;

namespace UnicomTIC_LMS_BE.IRepositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        //Task<Role> GetRoleByNameAsync(string roleName);
        //Task AddUserRoleAsync(UserRole userRole);
        //Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailForgotPassword(string email);
        Task<OTP> SaveOTP(OTP oTP);
        Task<OTP> CheckOTPExits(string otp);
        Task<User> ChangePassword(ChangePasswordDTO dTO);
    }
}
