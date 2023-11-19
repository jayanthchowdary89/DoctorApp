using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Dto
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public DoctorDto Doctor { get; set; }
        public PatientDto Patient { get; set; }

        [DataType(DataType.Time)]
        public string Time_slot { get; set; }

        // [DataType(DataType.Currency)]
        public int Appointment_Fee { get; set; }
    }
}
