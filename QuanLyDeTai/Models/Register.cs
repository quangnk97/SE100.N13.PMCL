using System;
using System.Collections.Generic;

namespace QuanLyDeTai.Models
{
    public partial class Register
    {
        public int LecturerId { get; set; }
        public int TopicId { get; set; }
        public string? PositionTopic { get; set; }
        public int? Score { get; set; }
        public string? LevelLecturer { get; set; }

        public virtual Lecturer Lecturer { get; set; } = null!;
        public virtual Topic Topic { get; set; } = null!;
    }
}
