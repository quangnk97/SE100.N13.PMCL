using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QuanLyDeTai.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Assesses = new HashSet<Assess>();
            Registers = new HashSet<Register>();
        }

        public int TopicId { get; set; }
        public string? TopicName { get; set; }
        public int? RegisteredYear { get; set; }
        public string? ResearchField { get; set; }
        public string? LevelTopic { get; set; }
        public int? Duration { get; set; }
        public int? AssessmentId { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public string? Comment { get; set; }
        public string? Rating { get; set; }
        public string? StatusId { get; set; }
        public bool? Approved { get; set; }
        public string? Reason { get; set; }
        public bool? IsExtended { get; set; }
        public int? IsCancelled { get; set; }
        public int? RequestTime { get; set; }

        public virtual Assessment? Assessment { get; set; }
        public virtual Status? Status { get; set; }
        public virtual ICollection<Assess> Assesses { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Register> Registers { get; set; }
    }
}
