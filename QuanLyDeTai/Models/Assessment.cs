using System;
using System.Collections.Generic;

namespace QuanLyDeTai.Models
{
    public partial class Assessment
    {
        public Assessment()
        {
            Assesses = new HashSet<Assess>();
            Topics = new HashSet<Topic>();
        }

        public int AssessmentId { get; set; }
        public int? NumberOfMembers { get; set; }

        public virtual ICollection<Assess> Assesses { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
