using DoctorApp.Models;
using DoctorApp.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersLogic _usersLogic;

        private readonly DoctorAppDbContext _context;

        public LoginController(IUsersLogic usersLogic, DoctorAppDbContext context)
        {
            _usersLogic = usersLogic;
            _context = context;

        }
        

        [HttpPost]
        public  IActionResult LoginAuthentication([FromBody] Login model)
        {
             Patient User = _usersLogic.ValidateCredentials(model.Username, model.Password);

            if (User == null)
            {
                return Unauthorized();
            }
            else {
               var token = _usersLogic.GenerateJwtToken(User);
                return Ok(new { Token = token });
            }
            
        }


        [HttpPost("Doctor")]

        public IActionResult LoginAuthenticationForDoc([FromBody] Login model)
        {
            Doctor User = _usersLogic.ValidateCredentialsForDoc(model.Username, model.Password);

            if (User == null)
            {
                return Unauthorized();
            }
            else
            {

                var token = _usersLogic.GenerateJwtTokenForDoc(User);
                return Ok(new { Token = token });
            }

        }


    }
}
