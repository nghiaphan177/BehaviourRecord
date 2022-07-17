namespace BehaviourManagementSystem_API.Utilities
{
	/// <summary>
	/// Check all request illegal
	/// Writer: DuyLH4
	/// </summary>
	public static class CheckRequestExtensions
	{
		public static bool CheckRequest(this string str) => string.IsNullOrEmpty(str);
		public static bool CheckRequest(this object obj) => obj is null;
		public static bool CheckUserNameRepuest(this string str)
		{
			var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
			var count = 0;
			foreach(var i in str)
			{
				foreach(var j in chars)
				{
					if(i == j)
					{
						count++;
						break;
					}
				}
				if(count == str.Length)
					return true;
			}
			return false;
		}
		public static bool CheckPaswordRepuest(this string str)
		{
			if(str.Length < 6 || str.Length > 18)
				return false;
			var count = 0;

			foreach(var item in str)
			{
				if(char.IsDigit(item))
				{
					count++;
					break;
				}
			}

			foreach(var item in str)
			{
				if(char.IsUpper(item))
				{
					count++;
					break;
				}
			}

			foreach(var item in str)
			{
				if(char.IsLower(item))
				{
					count++;
					break;
				}
			}

			foreach(var item in str)
			{
				if(char.IsLetterOrDigit(item))
				{
					count++;
					break;
				}
			}
			if(count >= 4)
				return true;
			return false;
		}
	}
}
