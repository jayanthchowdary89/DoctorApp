using DoctorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DoctorApp.Repository
{
    public class DocRepo : IDocRepo
    {
        private readonly DoctorAppDbContext _Context;
        public DocRepo(DoctorAppDbContext Context) { 

            _Context = Context;
        }

        public  async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors() {
        
              return await _Context.Doctors.ToListAsync();
        }

        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _Context.Doctors.FindAsync(id);

            return doctor;
        }


    }
}
