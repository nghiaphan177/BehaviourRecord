using System;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model TermCondition
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class TermCondition
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}