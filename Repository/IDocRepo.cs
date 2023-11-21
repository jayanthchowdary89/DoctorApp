using DoctorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApp.Repository
{
    public interface IDocRepo
    {
        Task<ActionResult<IEnumerable<Doctor>>> GetDoctors();
        Task<ActionResult<Doctor>> GetDoctor(int id);
    }
}
