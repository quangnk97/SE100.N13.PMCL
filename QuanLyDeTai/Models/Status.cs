using System;
using System.Collections.Generic;

namespace QuanLyDeTai.Models
{
    public partial class Status
    {
        public Status()
        {
            Topics = new HashSet<Topic>();
        }

        public string StatusId { get; set; } = null!;
        public string? StatusName { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
