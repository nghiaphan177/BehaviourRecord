using System;

namespace BehaviourManagementSystem_ViewModels.Requests
{
	public class UserProfileRequest
	{
		public string Id { get; set; }
		
		public string UserName { get; set; }
		
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		public string Gender { get; set; }
		
		public DateTime? DOB { get; set; }
		
		public string PhoneNumber { get; set; }
		
		public string Email { get; set; }
		
		public string Address { get; set; }
		
		public string AvtName { get; set; }
		
		public string RoleId { get; set; }
		
		public string RoleName { get; set; }

        public bool? Active { get; set; }
       
		public string TeacherId { get; set; }
    }
}
