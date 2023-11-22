using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorApp.Models;
using DoctorApp.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DoctorApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        private readonly IDocRepo _Repo;

        public DoctorsController(DoctorAppDbContext context,IDocRepo Repo)
        {
            _context = context;
            _Repo = Repo;
        }

        //GET: api/Doctors
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            //return await _context.Doctors.ToListAsync();

            return await _Repo.GetDoctors();
        }

        // GET: api/Doctors/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            //var doctor = await _context.Doctors.FindAsync(id);
            var doctor = await _Repo.GetDoctor(id);

            if (doctor == null)
            {
                return NotFound();
            }
            return doctor;
        }


        //[Authorize]
        [Authorize]
        [HttpGet("GetDoctor/{username}")]

        public ActionResult<Doctor> GetDoctorUser(string username)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = _context.Doctors.FirstOrDefault(o => o.D_UserId == username);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }


        [Authorize]

        [HttpGet("GetDoctorBySpecialization/{dept}")]

        public ActionResult<IEnumerable<Doctor>> GetDoctorBySpecialization(string dept)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor =  _context.Doctors.Where(o => o.Specialization == dept).ToList();

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.DId)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
          if (_context.Doctors == null)
          {
              return Problem("Entity set 'DoctorAppDbContext.Doctors'  is null.");
          }
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.DId }, doctor);
        }

        // DELETE: api/Doctors/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize]
        [HttpGet("GetCurrent")]
        public IActionResult GetDoctorsProfile()
        {
            var currentuser = GetCurrentUserInfo();
            return Ok(currentuser);
        }
        private UserInfo GetCurrentUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new UserInfo{ 

                 D_UserId = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.NameIdentifier).Value,

                 DName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name).Value,

                 Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value,

                 Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role).Value,

                 Dimg = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.Uri).Value,

                 DId = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid).Value
                };
            }
            // Retrieve user claims from the authenticated user's identity
            return null;

        }


            private bool DoctorExists(int id)
        {
            return (_context.Doctors?.Any(e => e.DId == id)).GetValueOrDefault();
        }
    }
}
