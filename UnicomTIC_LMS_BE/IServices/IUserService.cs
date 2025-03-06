using UnicomTIC_LMS_BE.Entities;
using UnicomTIC_LMS_BE.Models;

namespace UnicomTIC_LMS_BE.IServices
{
    public interface IUserService
    {
        Task<(string Token, User user)> Authenticate(string email, string password);
        Task<bool> SentOTP(string email);
        Task<bool> CheckOTP(string otp);
        Task<bool> ChangePassword(ChangePasswordDTO dTO);
    }
}
