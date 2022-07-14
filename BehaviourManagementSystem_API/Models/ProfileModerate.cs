﻿using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model ProfileModerate
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class ProfileModerate
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Intervention> Interventions { get; set; }
    }
}