using DoctorApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using DoctorApp.UserServices;
using System.Security.Cryptography;

namespace DoctorApp.UserServices
{
    public class UsersLogic : DbContext,IUsersLogic

    {
        private readonly IConfiguration _config;
        public UsersLogic(IConfiguration config, DoctorAppDbContext context)
        {
            _config = config;
            _context = context;
        }

        private static DoctorAppDbContext _context;

   
        public  bool VerifyPassword(string Password, string EnteredPassword)
        {
            if (Password == EnteredPassword)
            {
                return true;
            }
            return false;
        }

        public  Patient ValidateCredentials(string username, string password)
        {
            var user = _context.Patients.FirstOrDefault(u => u.P_UserId == username);

            // If the user is not found or the password is incorrect, return false
            if (user == null || !VerifyPassword(user.Password, password))
            {
                return null;
            }


            return user;
        }
       
        

//// Generate a random 256-bit (32-byte) key
//byte[] keyBytes = new byte[32];
//using (var rng = new RNGCryptoServiceProvider())
//{
//    rng.GetBytes(keyBytes);
//}

public string  GenerateJwtToken(Patient user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.P_UserId),
               // new Claim(ClaimTypes.Role,user.Role)
                new Claim(ClaimTypes.Name,user.PName),
                new Claim(ClaimTypes.Email,user.Email)

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }





}

