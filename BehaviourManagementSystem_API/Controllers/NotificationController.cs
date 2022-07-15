using BehaviourManagementSystem_API.Services;
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

		[HttpGet("noall/{id}")]
		public async Task<IActionResult> GetAll(string id)
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			var response = await _notificationService.GetAllByUserId(id);

			if(response.Result == null)
				return BadRequest(response);

			return Ok(response);
		}
	}
}
