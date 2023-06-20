using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
//using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using ToyStoreAPI.Helpers;
using ToyStoreAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly EmailHelper _emailHelper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, EmailHelper email)
        {
            _emailHelper = email;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(new Response { Status = "Success", Message = "Logout successful!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });

            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and confirm new password does not match!" });

            var result = await _userManager.ChangePasswordAsync(userExists, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = "Something went wrong", Errors = result.Errors.Select(e => e.Description).ToList() });

            return Ok(new Response { Status = "Success", Message = "Password Changed successfully!" });
        }

        [HttpPost]
        [Route("reset-password-token/{email}")]
        public async Task<IActionResult> ResetPasswordToken(string email)
        {
                var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodeToken = Encoding.UTF8.GetBytes(token);
            var validToken=WebEncoders.Base64UrlEncode(encodeToken);

            var url = $"https://localhost:7124/reset-password?email={email}&token={validToken}";

            //_emailHelper.SendEmailResetPassword(email, @"
            //<html>
            //<head>
            //    <style>
            ///* Thiết lập các kiểu định dạng CSS */
            //        body {
            //            font-family: Arial, sans-serif;
            //        }
            //        .container {
            //            max-width: 600px;
            //            margin: 0 auto;
            //            padding: 20px;
            //        }
            //        .logo {
            //            text-align: center;
            //            margin-bottom: 20px;
            //        }
            //        .logo img {
            //            max-width: 200px;
            //        }
            //        h1 {
            //            text-align: center;
            //            color: #333;
            //        }
            //        p {
            //            line-height: 1.5;
            //            margin-bottom: 10px;
            //        }
            //        .button {
            //            display: inline-block;
            //            padding: 10px 20px;
            //            color: #fff;
            //            text-decoration: none;
            //            bordelm r-radius: 4px;
            //        }
            //    </style>
            //</head>
            //<body>
            //    <div class=""container"">
            //        //<div class=""logo"">
            //        //    <img src="""" alt=""Logo"">
            //        //</div>
            //        <h1>Reset Your Password</h1>
            //        <p>You have requested to reset your password. Please click on the link below:</p>
            //        <p><a href=""" + url + @""" class=""button"">Reset Password</a></p>
            //        <p>If you did not request this, please ignore this email.</p>
            //        <p>Best regards,</p>
            //        <p>ToyStore</p>
            //    </div>
            //</body>
            //</html>");

            _emailHelper.SendEmailResetPassword(email, @"
                <html>
                <body>
                    <h1>Reset Your Password</h1>
                    <p>You have requested to reset your password. Please click on the link below:</p>
                    <p><a href=""" + url + @""">Reset Password</a></p>
                    <p>If you did not request this, please ignore this email.</p>
                    <p>Best regards,</p>
                    <p>ToyStore</p>
                </body>
                </html>");
            return Ok(new Response { Status = "Success", Message = "Reset password URL has been sent to the email successfully!" });
        }
        
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });

            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and confirm new password does not match!" });
        
            var decodeToken = WebEncoders.Base64UrlDecode(model.Token!);
            string normalToken = Encoding.UTF8.GetString(decodeToken);

            var result = await _userManager.ResetPasswordAsync(userExists, normalToken, model.NewPassword);

            if (result.Succeeded)
                return Ok(new Response { Status = "Success", Message = "Password Reseted successfully!" });

            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            { Status = "Error", Message = "Something went wrong", Errors = result.Errors.Select(e => e.Description).ToList() });
        }
    }
}
