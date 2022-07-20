using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model Individual
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class Individual
    {
        public Guid Id { get; set; }
        public Guid? UserTeacherId { get; set; }
        public string Organization { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Assesetment> Assesetments { get; set; } = null!;
    }
}