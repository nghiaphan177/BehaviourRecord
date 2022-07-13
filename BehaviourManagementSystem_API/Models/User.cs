using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model User extends IdentityUser
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public byte[] Img { get; set; }
        public bool Activity { get; set; }
        public DateTime ActivityDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; } = null!;
        public virtual ICollection<Individual> Individuals { get; set; } = null!;
    }
}
