using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;
		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpGet("Notification/{id}")]
		public async Task<IActionResult> GetAll(string id)
		{
			if(id.CheckRequest())
				return BadRequest(id);

			var response = await _notificationService.GetAllByUserId(id);

			if(response.Result == null)
				return BadRequest(response);

			return Ok(response);
		}
	}
}
