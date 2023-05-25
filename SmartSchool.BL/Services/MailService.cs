using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SmartSchool.BL.Services
{
	public interface IMailService
	{
		Task SendEmailAsync(string toemail, string subject, string content);
	}
	public class MailService : IMailService
	{
		private readonly IConfiguration configuration;

		public MailService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task SendEmailAsync(string toemail, string subject, string content)
		{
			var apiKey = configuration["SendGridAPIKey"];
			var client = new SendGridClient(apiKey);
			var from = new EmailAddress("Smartschool045@gmail.com", "Admin of Smart School");
			var to = new EmailAddress(toemail);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
			var response = await client.SendEmailAsync(msg);
		}
	}
}
