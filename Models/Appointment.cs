using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Models
{
    public class Appointment
    {

        public enum AppointmentStatus
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2,
            Rescheduled = 3,
            Completed = 4,
        }
        [Key]
        public int AId { get; set; }
        [ForeignKey("Doctor")]
        public int? DId { get; set; }
        [ForeignKey("User")]
        public int? PId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Appointment_Date { get; set; }

        [DataType(DataType.Time)]
        public string Time_slot { get; set; }

        [DataType(DataType.Currency)]
        public decimal Appointment_Fee { get; set; }
        public bool? Is_approved { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
