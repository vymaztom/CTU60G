using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;
using Newtonsoft.Json;

/****************************************************************************************************  

                                   EXAMPLE HOW TO USE CLASS EmailService
 
***************************************************************************************************/

/*    

    EmailService email = new EmailService("EmailConfig.json");

    try
    {
        email.Send("Subjects", "Body");
    }
    catch (Exception e)
    {
        // ADD note into log
        Console.WriteLine(e.Message);
    }

*/

/****************************************************************************************************  

                                   STRUCTURE OF MailConfig.json
 
***************************************************************************************************/

/*    

  {
    "Host": "smtp.gmail.com",                               // GMAIL DEFAULT host 
    "Port": "587",                                          // STANDART SMTP port
    "EnableSsl": "true",                                    // MUST TO BE true, IF NOT send will not work
    "User": "GMAIL MAIL ACOUNT FOR EXAPMLE (testtoscrpts@gmail.com)",
    "Password": "Password of GMAIL MAIL ACOUNT",
    "FromEmail": "EMAIL OF APT (not to be valide)",
    "FromName": "NAMEOFAPT",
    "ToEmails": [                                           // ARRAY OF emails to send msg
      "testtoscrpts@gmail.com",
      "testtoscrpts@gmail.com"
    ]
  }

 */

namespace CTU60G
{
    public class EmailService
    {
        public class EmailConfig
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string EnableSsl { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string FromEmail { get; set; }
            public string FromName { get; set; }
            public IList<string> ToEmails { get; set; }
        }

        private bool isHTML;
        private MailPriority priority;
        private String pathToConfigFile;

        public EmailService(String PathToConfigFile, MailPriority Priority, bool IsHTLMmsg)
        {
            pathToConfigFile = PathToConfigFile;
            isHTML = IsHTLMmsg;
            priority = Priority;
        }
        public EmailService(String PathToConfigFile)
        {
            pathToConfigFile = PathToConfigFile;
            isHTML = false;
            priority = MailPriority.High;
        }

        private MailMessage createMsg(String subject, String body)
        {
            MailMessage msg = new MailMessage();
            msg.Priority = priority;
            msg.Subject = subject;
            msg.Body = body;
            return msg;
        }

        private static MailAddress createMailAddress(String email, String name)
        {
            return new MailAddress(email, name);
        }

        private EmailConfig loadConfig()
        {
            EmailConfig item;
            using (StreamReader r = new StreamReader(pathToConfigFile))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<EmailConfig>(json);
            }
            return item;
        }

        private void sendEmail(MailMessage msg)
        {
            EmailConfig conf = loadConfig();

            msg.From = createMailAddress(conf.FromEmail, conf.FromName);
            foreach (String item in conf.ToEmails)
            {
                msg.To.Add(item);
            }

            SmtpClient client = new SmtpClient();
            client.Host = conf.Host;
            client.Port = conf.Port;
            if (conf.EnableSsl == "true")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(conf.User, conf.Password);
            client.Send(msg);
        }

        public void Send(String subject, String body)
        {
            MailMessage msg = createMsg(subject, body);
            sendEmail(msg);
        }

    }
}
