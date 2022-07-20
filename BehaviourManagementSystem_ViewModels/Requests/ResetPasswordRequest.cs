using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_ViewModels.Requests
{
	/// <summary>
	/// ConfirmEmailRequest view model
	/// Writer: DuyLH4
	/// </summary>
	public class ResetPasswordRequest
	{
		public string Id { get; set; }
		public string UserOrEmail { get; set; }
		public string Code { get; set; }
		public string PasswordOld { get; set; }
		public string PasswordNew { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
