
using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
	/// <summary>
	/// Class NotificationService Implement INotificationService. Design parttern repository.
	/// WriterL DuyLH4
	/// </summary>
	public class NotificationService : INotificationService
	{
		private readonly ApplicationDbContext _context;
		public NotificationService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseResult<List<Notification>>> GetAllByUserId(string id)
		{
			var user = await _context.Users.FindAsync(new Guid(id));
			if(user == null)
				return new ResponseResultError<List<Notification>>("Tài khoản không tồn tại");
			return new ResponseResultSuccess<List<Notification>>(
				await _context.Notifications.Where(prop => prop.UserId == user.Id).ToListAsync());
		}
	}
}
