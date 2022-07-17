using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_ViewModels.Requests
{
	/// <summary>
	/// user request view model.
	/// Writer: DuyLH4
	/// </summary>
	public class UserProfileRequest
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime DOB { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public byte[] Img { get; set; }
		public string RoleId { get; set; }
		public string RoleName { get; set; }
	}
}
