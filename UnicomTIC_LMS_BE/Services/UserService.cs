using UnicomTIC_LMS_BE.Entities.Email;
using UnicomTIC_LMS_BE.Entities;
using UnicomTIC_LMS_BE.IRepositories;
using UnicomTIC_LMS_BE.Models.RequestModel;
using UnicomTIC_LMS_BE.Models;
using UnicomTIC_LMS_BE.IServices;

namespace UnicomTIC_LMS_BE.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly SendMailService _sendMailService;

        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository, SendMailService sendMailService)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _sendMailService = sendMailService;
        }

        public async Task<(string Token, User user)> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var role = user.UserRole?.Role?.RoleName ?? throw new Exception("User role not found");

            var token = _tokenRepository.GenerateToken(user);

            return (token, user);
        }

        public async Task<bool> SentOTP(string email)
        {
            //var checkUserExits = await _userRepository.GetUserByEmailForgotPassword(email);
            //if (checkUserExits == null) throw new Exception("User Not Found");

            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            var today = DateTime.UtcNow;
            var expirationTime = DateTime.UtcNow.AddMinutes(7);

            var OTP = new OTP
            {
                UserID = Guid.NewGuid(),
                Email = email,
                Code = otp,
                StartTime = today,
                EndTime = expirationTime,
            };

            var mail = new SendMailRequest
            {
                OTP = otp,
                Email = email,
                EmailType = EmailType.OTP,
            };

            await _userRepository.SaveOTP(OTP);
            await _sendMailService.SendMail(mail);
            return true;
        }

        public async Task<bool> CheckOTP(string otp)
        {
            var exits = await _userRepository.CheckOTPExits(otp);
            if (exits == null) throw new Exception("OTP not found");
            var today = DateTime.UtcNow;
            if (exits.EndTime < today) throw new Exception("OTP time out");
            return true;

        }

        public async Task<bool> ChangePassword(ChangePasswordDTO dTO)
        {
            var data = await _userRepository.ChangePassword(dTO);
            return data != null ? true : false;
        }
    }
}
