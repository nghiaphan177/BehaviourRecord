using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    public class ProfileRecovery
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Intervention> Interventions { get; set; } = null!;
    }
}