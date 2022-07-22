using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    public class AnalyzeAntecedentPerceive
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Assessment> Assesetments { get; set; } = null!;
    }
}