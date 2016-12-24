using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace Duty2.Helpers
{
    public class EmailSender
    {
        public void SendMailGlobal(string  theme, string body, string sendTo)
        {

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("2gis\\duty", "123qwe!!");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("duty@2gis.ru");

            smtpClient.Host = "mx.2gis.local";
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = "График дежурств";

            message.IsBodyHtml = true;
            message.Body = body + "<br/>" + "<br/>" + " " + body;

            message.To.Add(sendTo);

            smtpClient.Send(message);
        }

    }
}