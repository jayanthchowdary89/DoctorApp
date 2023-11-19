using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorApp.Models;
using DoctorApp.Dto;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly DoctorAppDbContext _context;

        public AppointmentsController(DoctorAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
          if (_context.Appointments == null)
          {
              return NotFound();
          }
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointments/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Appointment>> GetAppointment(int id)
        //{
        //  if (_context.Appointments == null)
        //  {
        //      return NotFound();
        //  }
        //    var appointment = await _context.Appointments.FindAsync(id);

        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    return appointment;
        //}

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.AId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
          if (_context.Appointments == null)
          {
              return Problem("Entity set 'DoctorAppDbContext.Appointments'  is null.");
          }
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.AId }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.AId == id)).GetValueOrDefault();
        }



        [HttpPost("create")]
        public IActionResult CreateAppointment([FromBody] AppointmentCreateDto appointmentDto)
        {
            try
            {
                // Check if the Doctor and Patient with the given IDs exist
                var existingDoctor = _context.Doctors.Find(appointmentDto.DoctorId);
                var existingPatient = _context.Patients.Find(appointmentDto.PatientId);

                if (existingDoctor == null || existingPatient == null)
                {
                    return NotFound("Doctor or Patient not found");
                }

                // Create a new Appointment using the provided data
                var newAppointment = new Appointment
                {
                    Appointment_Date = appointmentDto.AppointmentDate,
                    DId = appointmentDto.DoctorId,
                    PId = appointmentDto.PatientId,
                    Time_slot = appointmentDto.Time_slot,
                    Appointment_Fee= appointmentDto.Appointment_Fee
                    
                };

                // Add the new appointment to the context and save changes
                _context.Appointments.Add(newAppointment);
                _context.SaveChanges();

                // Optionally, you can return the created appointment details
                var createdAppointmentDto = new AppointmentDto
                {
                    AppointmentId = newAppointment.AId,
                    Appointment_Date = newAppointment.Appointment_Date,
                    Time_slot = newAppointment.Time_slot,
                    Appointment_Fee = newAppointment.Appointment_Fee,
                    Doctor = new DoctorDto { DoctorId = existingDoctor.DId, Name = existingDoctor.DName },
                    Patient = new PatientDto { PatientId = existingPatient.PId, Name = existingPatient.PName }
                };

                return Ok(createdAppointmentDto);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, "Internal Server Error");
            }
        }


        //new GET end point using DTO 
        [HttpGet("{id}")]
        public IActionResult GetAppointment(int id)
        {
            try
            {
                // Find the appointment by ID
                var appointment = _context.Appointments
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient)
                    .FirstOrDefault(a => a.AId == id);

                if (appointment == null)
                {
                    return NotFound("Appointment not found");
                }

                // Map the appointment to AppointmentDto for response
                var appointmentDto = new AppointmentDto
                {
                    AppointmentId = appointment.AId,
                    Appointment_Date = appointment.Appointment_Date,
                    Time_slot= appointment.Time_slot,
                    Appointment_Fee = appointment.Appointment_Fee,
                    Doctor = new DoctorDto { DoctorId = appointment.Doctor.DId, Name = appointment.Doctor.DName },
                    Patient = new PatientDto { PatientId = appointment.Patient.PId, Name = appointment.Patient.PName }
                };

                return Ok(appointmentDto);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
