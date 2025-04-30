using CRUDify_API.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using CRUDify_API.Repositories.Concrete;
using CRUDify_API.Entities;


namespace CRUDify_API.Controller.cs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ILogRepository _logRepository;

        public AuthsController(IUserRepository userRepository, IAuthRepository authRepository, ILogRepository logRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _logRepository = logRepository;
        }

        [HttpGet("checkuserbyemail")]
        public async Task<IActionResult> CheckUserByEmail(string email, string password)
        {
            var dbResult = _authRepository.CheckUserByEmail(email);
            if (dbResult)
            {
                try
                {
                    Console.WriteLine($" Checking user: {email}");

                    var authResult = _authRepository.LoginWithEmailAndPassword(email, password);

                    if (!authResult)
                    {
                        Console.WriteLine("Authentication failed for user: " + email);
                        return BadRequest(new
                        {
                            message = "Authentication failed for user: " + email,
                            success = false
                        });
                    }

                    Console.WriteLine("Authentication successful for user: " + email);
                    User user = new User();
                    user = await _userRepository.GetUserByEmail(email);
                    await _logRepository.LogUserActivity(user.UserId, "Login", null);
                    return Ok(new
                    {
                        message = "Authentication successful",
                        _user = user,
                        success = true,
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Exception in CheckUserByEmail: {ex.Message}");
                    return StatusCode(500, new { message = "Internal server error", error = ex.Message });
                }
            }
            else
            {
                return BadRequest(new
                {
                    message = $"{email} uygulamamıza kayıtlı değildir. Lütfen kayıt oluşturunuz.",
                    success = false
                });
            }
        }
    }
}
