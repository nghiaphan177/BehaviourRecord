﻿using System;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model Notification
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class Notification
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string ContentHTML { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid? UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}