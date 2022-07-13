using Microsoft.AspNetCore.Identity;
using System;

namespace BehaviourManagementSystem_API.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            this.Id = Guid.NewGuid();
        }
    }
}