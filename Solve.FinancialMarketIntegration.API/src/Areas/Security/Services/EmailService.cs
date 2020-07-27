using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public class EmailService : IEmailService
    {
        public EmailSettings EmailSettings { get; }
        private IHostingEnvironment Environment { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, IHostingEnvironment environment)
        {
            EmailSettings = emailSettings.Value;
            Environment = environment;
        }

        public Task SendEmailAsync(string emailTo, 
            string subject,
            string message = null,
            EmailTemplateEnum? templateEnum = null,
            Dictionary<string, string> valoresReplace = null)
        {
            try
            {
                Execute(emailTo, subject, message, templateEnum, valoresReplace).Wait();

                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Execute(string emailTo, 
            string subject,
            string message = null,
            EmailTemplateEnum? templateEnum = null, 
            Dictionary<string, string> valoresReplace = null)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(EmailSettings.FromEmail, EmailSettings.FromUser)
            };

            mail.To.Add(new MailAddress(emailTo));

            mail.Subject = subject;

            if(string.IsNullOrEmpty(message))
            {
                var nomeArquivo = templateEnum.GetDescription();

                var path = GetPath(nomeArquivo);

                var mailBody = string.Empty;

                using (StreamReader sr = new StreamReader(path))
                {
                    mailBody = sr.ReadToEnd();
                }

                foreach (var chaveValor in valoresReplace)
                {
                    mailBody = mailBody.Replace(chaveValor.Key, chaveValor.Value);
                }

                message = mailBody;
            }

            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(EmailSettings.PrimaryDomain, EmailSettings.PrimaryPort))
            {
                smtp.EnableSsl = false;

                await smtp.SendMailAsync(mail);
            }
        }

        private string GetPath(string filename)
        {
            string path = "Areas/Security/Templates";
            string rootPath = this.Environment.WebRootPath ?? this.Environment.ContentRootPath;
            return System.IO.Path.Combine(rootPath, path, filename);
        }
    }
}
