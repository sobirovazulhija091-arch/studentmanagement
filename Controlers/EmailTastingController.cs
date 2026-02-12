using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/email")]
public class EmailTastingController(IEmailService service,UserManager<ApplicationUser> userManager) : ControllerBase
{
     private readonly IEmailService _emailService = service;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpPost]
        public async Task<IActionResult> SendTestEmail()
        {
            await _emailService.SendAsync("sobirovazulhija2@gmail.com",
            "Testing SMTP",
            "Salom, if you are reading this, it is ok!");
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
        
            if (user == null)
                return Ok();
        
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
            var link = $"http://localhost:5053/api/EmailTesting/reset-password?email={dto.Email}&token={Uri.EscapeDataString(token)}";
        
            await _emailService.SendAsync(dto.Email,
                "Reset Password",
                $"Reset link: {link}");
        
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }


