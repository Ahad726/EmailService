using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _client;
        private string _senderEmail;
        
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            var emailConfiguration = _configuration.GetSection("EmailConfig");
            _senderEmail = emailConfiguration.GetSection("Username").Value;

            _client = new SmtpClient();
            _client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            _client.Connect(_configuration["EmailConfig:Host"],
                int.Parse(_configuration["EmailConfig:Port"]),
                bool.Parse(_configuration["EmailConfig:UseSSL"]));
            _client.Authenticate(_configuration["EmailConfig:Username"], _configuration["EmailConfig:Password"]);
        }


        public void SendEmail(List<Message> messages)
        {
            try
            {
                foreach (var message in messages)
                {
                    var mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress("Stay Home", _senderEmail));
                    mail.To.Add(new MailboxAddress(string.Empty, message.Receiver));
                    mail.Subject = message.Subject;
                    mail.Body = new TextPart(TextFormat.Text)
                    {
                        Text = message.Body
                    };

                    _client.Send(mail);
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
