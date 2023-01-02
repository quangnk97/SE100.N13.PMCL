using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuanLyDeTai.Models
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            Assesses = new HashSet<Assess>();
            Registers = new HashSet<Register>();
        }

        public int LecturerId { get; set; }
        public string? LecturerCode { get; set; }
        public string? Password { get; set; }
        public string? LecturerName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Major { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Contract { get; set; }
        public string? LevelCurrent { get; set; }
        public string? FacultyId { get; set; }
        public string? Avatar { get; set; }

        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<Assess> Assesses { get; set; }
        public virtual ICollection<Register> Registers { get; set; }
    }
}
