using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace UniFinder.Class
{
    public class Email
    {
        public void EmailSend(string email)
        {
            try
            {

                if (email != null)
                {
                    string subject = "Registration";
                    string Msg = "Your registration request has been approved by administrator <br /><br />"
                        + "<b>Thanks & Regards</b><br/>" + "<b>Uni Finder Admin</b>";
                    MailMessage message = new MailMessage("mshakir628@gmail.com", email, subject, Msg);
                    message.IsBodyHtml = true;
                    message.IsBodyHtml = true;
                    SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
                    Client.EnableSsl = true;
                    Client.Credentials = new NetworkCredential("mshakir628@gmail.com", "rdpg mgkt lztt xygf");
                    Client.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
     
    }
}