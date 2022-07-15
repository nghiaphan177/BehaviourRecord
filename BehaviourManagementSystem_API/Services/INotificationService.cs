using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
	public interface INotificationService
	{
		Task<ResponseResult<List<Notification>>> GetAllByUserId(string id);
	}
}