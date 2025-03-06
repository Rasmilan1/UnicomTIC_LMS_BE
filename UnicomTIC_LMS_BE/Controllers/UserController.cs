using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnicomTIC_LMS_BE.IServices;
using UnicomTIC_LMS_BE.Models;

namespace UnicomTIC_LMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("SentOTP")]
        public async Task<IActionResult> SentOTP(string email)
        {
            var data = await _userService.SentOTP(email);
            var json = new { message = "OTP sented Succesfully" };
            return Ok(json);
        }
        [HttpPost("CheckOTP")]
        public async Task<IActionResult> CheckOTP(string otp)
        {
            var data = await _userService.CheckOTP(otp);
            var json = new { message = "OTP verifyed Succesfully" };
            return Ok(json);

        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dTO)
        {
            var data = await _userService.ChangePassword(dTO);
            var json = new { message = "PasswordChanged" };
            return Ok(json);
        }
    }
}
