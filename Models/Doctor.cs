﻿using System.ComponentModel.DataAnnotations;

namespace DoctorApp.Models
{
    public class Doctor
    {
        public enum genderbar1
        {
            Male = 0,
            Female = 1,
            Others = 2,
        }
        [Key]
        public int DId { get; set; }

        [Required]
        [MaxLength(20)]
        public string DName { get; set; }

        [Required]

        public int Age { get; set; }
        public genderbar1 Gender { get; set; }
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
        public string Experience { get; set; }
        public string Available { get; set; }
    }
}
