using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Models
{
    public class Doctor
    {
       
        [Key]
        public int DId { get; set; }

        [Required]
        [MaxLength(20)]
        public string DName { get; set; }

        [Required]

        public int Age { get; set; }
        public string Gender { get; set; }

        
        public string D_UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string Mobile { get; set; }


        public int Medical_Registration { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
      //  public string Available { get; set; }
        public string Dimg { get; set; }

        public string Description { get; set; }

        public string Designation {  get; set; }

        public string Qualification {  get; set; }

        public string Role { get; set; }

        //public ICollection<Appointment> Appointments { get; set; }
    }
}
