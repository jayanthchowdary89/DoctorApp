using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Models
{
    public class Patient
    {
        public enum genderbar
        {
            Male,
            Female,
            Other
        }
        [Key]

        public int PId { get; set; }
        public string PName { get; set; }

       [Range(1, 100)]
        public int Age { get; set; }
        public genderbar Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string P_UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string Mobile { get; set; }

        public string Role { get; set; } = "Patient";
    }
}
