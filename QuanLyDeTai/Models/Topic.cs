using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Name of Topic")]
        public string? TopicName { get; set; }
        [DisplayName("Year of Topic's Registration")]
        public int? RegisteredYear { get; set; }
        [DisplayName("Field of Topic")]
        public string? ResearchField { get; set; }
        [DisplayName("Level of Topic")]
        public string? LevelTopic { get; set; }
        [DisplayName("Duration for Doing Topic")]
        public int? Duration { get; set; }
        [DisplayName("Assessment ID")]
        public int? AssessmentId { get; set; }
        [DisplayName("Date of Assessment")]
        public DateTime? AssessmentDate { get; set; }
        [DisplayName("Comment of Chairman of Council")]
        public string? Comment { get; set; }
        [DisplayName("Rating For Topic")]
        public string? Rating { get; set; }
        [DisplayName("Status ID")]
        public string? StatusId { get; set; }
        [DisplayName("Is Topic approved?")]
        public bool? Approved { get; set; }
        [DisplayName("Reason for Status")]
        public string? Reason { get; set; }
        [DisplayName("Is Topic extended?")]
        public int? IsExtended { get; set; }
        [DisplayName("Is Topic cancelled?")]
        public int? IsCancelled { get; set; }
        [DisplayName("Time for Request")]
        public int? RequestTime { get; set; }

        public virtual Assessment? Assessment { get; set; }
        public virtual Status? Status { get; set; }
        public virtual ICollection<Assess> Assesses { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Register> Registers { get; set; }
    }
}
