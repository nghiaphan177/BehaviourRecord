using System;

namespace BehaviourManagementSystem_API.Models
{
    public class TermCondition
    {
        public Guid Id { get; set; }
       
        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }
    }
}