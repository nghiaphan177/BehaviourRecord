using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviourManagementSystem_API.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string DisplayName { get; set; }

        public string Gender { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        public string Address { get; set; } = null!;

        public string AvtName { get; set; } = null!;

        public bool Activity { get; set; }

        public DateTime? ActivityDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Individual> Individuals { get; set; } = null!;

    }
    public class Role : IdentityRole<Guid>
    {
    }
}
