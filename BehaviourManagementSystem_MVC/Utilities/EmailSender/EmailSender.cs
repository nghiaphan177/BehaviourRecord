using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_MVC.Utilities.EmailSender
{
	public class EmailSender: IEmailSender
	{
        private readonly EmailSettings _setting;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailSettings> setting, ILogger<EmailSender> logger)
        {
            _setting = setting.Value;
            _logger = logger;
            _logger.LogInformation("Tạo mail mới");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // tạo ra mail kèm đường dẫn mail from to và subject
            var message = new MimeMessage();

            message.Sender = new MailboxAddress(_setting.DisplayNameEmailSender, _setting.MailSender);
            message.From.Add(new MailboxAddress(_setting.DisplayNameEmailSender, _setting.MailSender));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            // tạo mail body dứ dạng html
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            using(var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    smtp.Connect(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_setting.MailSender, _setting.PasswordMailSender);
                    await smtp.SendAsync(message);
                }
                catch(Exception ex)
                {
                    // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                    System.IO.Directory.CreateDirectory("Mail_Logger");
                    var email_save_file = string.Format(@$"Mail_/{Guid.NewGuid()}.eml");
                    await message.WriteToAsync(email_save_file);

                    _logger.LogInformation("Lỗi gửi mail, lưu tại - " + email_save_file);
                    _logger.LogError(ex.Message);
                }
                smtp.Disconnect(true);
            }
            _logger.LogInformation("send mail to: " + email);
        }
    }
}
