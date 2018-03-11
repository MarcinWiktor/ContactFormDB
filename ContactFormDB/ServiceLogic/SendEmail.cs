using ContactFormDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ContactFormDB.ServiceLogic
{
    public class EmailService
    {
        private SmtpClient _smtpClient;
        private const string _login = "gym550182@gmail.com";
        private const string _pass = "!QAZ2wsx#EDC";
        public EmailService()
        {
            Initialize();
        }
        private void Initialize()
        {
            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("gym550182@gmail.com", "!QAZ2wsx#EDC")
            };
        }
        public MailMessage CreateMailMessage(ContactForm mailModel, bool isOwner = false)
        {
            var message = new MailMessage()
            {
                Sender = new MailAddress(_login),
                From = new MailAddress(_login),
                To = { mailModel.Email },
                Subject = "Thaks for Confirmation",
                Body = "Dzieki",
                IsBodyHtml = true
            };
            if (isOwner) return message;
            message.To.Add((_login));
            message.Subject = "Ktos potwierdził";
            message.Body = $"{mailModel.UserName} potwierdził przybycie";
            return message;
        }
        public void SendEmail(MailMessage mailModel)
        {
            _smtpClient.Send(mailModel);

        }
    }
}