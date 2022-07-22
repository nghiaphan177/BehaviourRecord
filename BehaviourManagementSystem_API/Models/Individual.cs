using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    public class Individual
    {
        public Guid Id { get; set; }
       
        public Guid? TeacherId { get; set; }
        
        public string Organization { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }

        public Guid StudentId { get; set; }
        
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Assessment> Assesetments { get; set; } = null!;
    }
}