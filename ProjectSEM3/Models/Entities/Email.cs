using Newtonsoft.Json;
using ProjectSEM3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ProjectSEM3.Models.Entities
{
    public class Email
    {
        public string MailFrom { get; set; }
        public List<string> MailTo { get; set; }
        public MailMessage MessageBody { get; set; }
        public SmtpClient Client { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostMail { get; set; }
        public int PortMail { get; set; }
        public bool SSL { get; set; }

        public Email()
        {
            MailFrom = "vxp20xx@gmail.com";
            UserName = "vxp20xx@gmail.com";
            Password = "lsahxzfrluhhhfue";
            MailTo = new List<string>();
            MessageBody = new MailMessage();
            SSL = true;
            PortMail = 25;
            HostMail = "smtp.gmail.com";//or another email sender provider
        }

        public class Wellcome
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Token { get; set; }
        }

        public class Exam
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public DateTime LateTime { get; set; }
        }

        public class Reject
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string JobLink { get; set; }
        }

        public class Congratulations
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public Entities.Exam.Res Exam { get; set; }
        }

        public string SendWellcome(Wellcome email)
        {
            MailTo.Add(email.UserName);
            MessageBody.Subject = "Wellcome Email";
            MessageBody.Priority = MailPriority.High;
            MessageBody.IsBodyHtml = true;

            var bodyFormat = "<h1>Wellcome {0}</h1> <br/> " +
                "<h3>Your account is: {1}</h3> <br/>" +
                "<h3>Your password is: {2}</h3> <br/>" +
                "<p>Thanks for your interest in joining XXXX XXXX XXXX! To complete your registration, we need you to verify your email address.</p>" +
                "<p>This is Email verify account. Please click link below to verify your account</p>" +
                "<span><b>Activate</b> <a class='nav-link text-dark'target='blank' href='https://localhost:7256/Home/ActivateEmail?email={3}&token={4}'>Link</a></span>";

            var encryptASCIIEmail = email.UserName.EncryptPassword();
            var encryptASCIIToken = email.Token.EncryptPassword();

            MessageBody.Body = string.Format(bodyFormat, email.Name, email.UserName, email.Password, encryptASCIIEmail, encryptASCIIToken);

            return SendEmail();
        }

        public string SendExam(Exam email)
        {
            MailTo.Add(email.UserName);
            MessageBody.Subject = "Exam schedule";
            MessageBody.Priority = MailPriority.High;
            MessageBody.IsBodyHtml = true;

            var bodyFormat = "<h1>Dear {0}</h1> <br/> " +
                "<h3>Your test will be start at: {1}</h3> <br/>" +
                "<h3>And end at: {2}</h3> <br/>" +
                "<p> <b>Please join in time. If you late 30 mins ({3}). Your test will fail automatically. </b> </p>";

            MessageBody.Body = string.Format(bodyFormat, email.Name, email.StartTime, email.EndTime, email.LateTime);

            return SendEmail();
        }

        public string SendReject(Reject email)
        {
            MailTo.Add(email.UserName);
            MessageBody.Subject = "Apologize Email";
            MessageBody.Priority = MailPriority.High;
            MessageBody.IsBodyHtml = true;

            var bodyFormat = "<h1>Dear {0}</h1> <br/> " +
                "<p>Thanks for your interest our job at: </p> <a class='nav-link text-dark'target='blank' href='{1}'>Link</a>" +
                "<p>Unfortunately your experience suitable for our job.</p> ";

            MessageBody.Body = string.Format(bodyFormat, email.Name, email.JobLink);

            return SendEmail();
        }

        public string SendCongratulations(Congratulations email)
        {
            MailTo.Add(email.UserName);
            MessageBody.Subject = "Congratulations Email";
            MessageBody.Priority = MailPriority.High;
            MessageBody.IsBodyHtml = true;

            var bodyFormat = "<h1>Congratulations {0}</h1> <br/> " +
                "<h3>You has passed the test on {1}</h3> <br/>" +
                "<h3>Total point: {2}</h3> <br/>";


            MessageBody.Body = string.Format(bodyFormat, email.Name, email.Exam.StartTime, email.Exam.TotalPoint);

            return SendEmail();
        }

        private string SendEmail()
        {
            try
            {
                var connectAccess = new SmtpPermission(SmtpAccess.Connect);
                Client = new SmtpClient(HostMail, PortMail)
                {
                    EnableSsl = SSL,
                    Credentials = new NetworkCredential(UserName, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 500000
                };

                MessageBody.Sender = new MailAddress(MailFrom);
                MessageBody.From = new MailAddress(MailFrom);
                foreach (string item in MailTo)
                {
                    MessageBody.To.Add(new MailAddress(item));
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                Client.UseDefaultCredentials = false;
                Client.EnableSsl = true;
                Client.Send(MessageBody);

                Client.SendAsyncCancel();
                MessageBody.Dispose();
                return JsonConvert.SerializeObject(new DbContext.Result
                {
                    Mes = "Send Email sucess.",
                    IsSuccess = true,
                });
            }

            catch (System.Net.Mail.SmtpException ex)
            {
                string err = ex.Message;
                return JsonConvert.SerializeObject(new DbContext.Result
                {
                    Mes = "Send email Fail.",
                    IsSuccess = false,
                });
            }
        }
    }
}