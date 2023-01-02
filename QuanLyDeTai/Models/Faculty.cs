using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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


        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}
