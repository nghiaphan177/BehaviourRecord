namespace BehaviourManagementSystem_MVC.Utilities.EmailSender
{
	public class EmailSettings
	{
        public string MailSender { get; set; }
        public string DisplayNameEmailSender { get; set; }
        public string PasswordMailSender { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string MailBodyHtml { get; set; }
    }
}
