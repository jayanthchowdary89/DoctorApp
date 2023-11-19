using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Dto
{
    public class AppointmentCreateDto
    {
        public DateTime? AppointmentDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        [DataType(DataType.Time)]
        public string Time_slot { get; set; }

        // [DataType(DataType.Currency)]
        public int Appointment_Fee { get; set; }

    }
}
