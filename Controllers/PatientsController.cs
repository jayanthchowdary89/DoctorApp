using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorApp.Models;
using System.Security.Claims;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        public PatientsController(DoctorAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            return await _context.Patients.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.PId)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
          if (_context.Patients == null)
          {
              return Problem("Entity set 'DoctorAppDbContext.Patients'  is null.");
          }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.PId }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetUser")]
        public IActionResult GetCurrentUserInfo()
        {
            // Retrieve user claims from the authenticated user's identity
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (userIdClaim != null && userNameClaim != null)
            {
                var userId = userIdClaim.Value;
                var userName = userNameClaim.Value;

                // Return user information
                var userInfo = new
                {
                    UserId = userId,
                    UserName = userName,
                };

                return Ok(userInfo);
            }

            // If claims are not found, the user might not be properly authenticated
            return Unauthorized();
        }

        private bool PatientExists(int id)
        {
            return (_context.Patients?.Any(e => e.PId == id)).GetValueOrDefault();
        }
    }
}
