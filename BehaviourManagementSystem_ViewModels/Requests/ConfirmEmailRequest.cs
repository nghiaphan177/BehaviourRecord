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
	public class ConfirmEmailRequest
	{
		[Required]
		public string Id { get; set; }
		[Required]
		public string Code { get; set; }
	}
}
