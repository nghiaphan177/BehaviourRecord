using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_ViewModels.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
		public bool Remeber { get; set; }
	}
}
