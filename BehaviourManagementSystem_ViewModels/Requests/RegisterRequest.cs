using System.ComponentModel.DataAnnotations;

namespace BehaviourManagementSystem_ViewModels.Requests
{
	/// <summary>
	/// RegisterRequest view model
	/// Writer: DuyLH4
	/// </summary>
	public class RegisterRequest
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}