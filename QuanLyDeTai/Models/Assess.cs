using System;
using System.Collections.Generic;

namespace QuanLyDeTai.Models
{
    public partial class Assess
    {
        public int AssessmentId { get; set; }
        public int LecturerId { get; set; }
        public int TopicId { get; set; }
        public string? PositionAssess { get; set; }
        public int? Scoring { get; set; }

        public virtual Assessment Assessment { get; set; } = null!;
        public virtual Lecturer Lecturer { get; set; } = null!;
        public virtual Topic Topic { get; set; } = null!;
    }
}
