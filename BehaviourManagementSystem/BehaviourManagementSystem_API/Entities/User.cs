using Microsoft.AspNetCore.Identity;
using System;

namespace BehaviourManagementSystem_API.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
