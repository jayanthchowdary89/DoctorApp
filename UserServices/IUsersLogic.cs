using DoctorApp.Models;

namespace DoctorApp.UserServices
{
    public interface IUsersLogic
    {
        bool VerifyPassword(string Password, string EnteredPassword);
        Patient ValidateCredentials(string username, string password);

        string GenerateJwtToken(Patient user);
    }
}
