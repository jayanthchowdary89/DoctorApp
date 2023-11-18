using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Models
{
    public class Appointment
    {

        public enum AppointmentStatus
        {
            Pending,
            Confirmed,
            Cancelled,
            Rescheduled,
            Completed,
        }
        [Key]
        public int AId { get; set; }
        
        public int DId { get; set; }
       // [ForeignKey("DId")]

      
        public int PId { get; set; }

        //[ForeignKey("PId")]
        

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
