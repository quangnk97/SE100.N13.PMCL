using System;
using System.Collections.Generic;

namespace QuanLyDeTai.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Lecturers = new HashSet<Lecturer>();
        }

        public string FacultyId { get; set; } = null!;
        public string? FacultyName { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}
