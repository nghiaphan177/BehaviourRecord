using Microsoft.AspNetCore.Identity;
using System;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model Role extends IdentityRole
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
    }
}