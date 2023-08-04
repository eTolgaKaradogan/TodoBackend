using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;

namespace TodoBackend.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(configuration["Mail:Username"], configuration["Mail:DisplayName"], Encoding.UTF8);

            SmtpClient smtpClient = new();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(configuration["Mail:Username"], configuration["Mail:Password"]);
            smtpClient.Port = Convert.ToInt32(configuration["Mail:Port"]);
            smtpClient.EnableSsl = true;
            smtpClient.Host = configuration["Mail:Host"];
            await smtpClient.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder body = new();
            body.Append("Merhaba,<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenilebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            body.Append(configuration["AngularClientUrl"]);
            body.Append("update-password/");
            body.Append(userId);
            body.Append("/");
            body.Append(resetToken);
            body.Append("\">Şifre yenilemek için tıklayınız..</a></strong><br><br><span style=\"font-size:12px;\"> NOT: Eğer şifre yenileme talebi size ait değilse bu maili ciddiye almayınız!</span><br><br><br> Tolga Todo!");

            await SendMailAsync(to, "Şifre Sıfırlama", body.ToString());
        }
    }
}
