using System;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
//using Intouch.Utils.Models;

namespace Intouch.Utils
{

    public class EmailController : Controller
    {
        
        public void Send(SendMailModel model)
        {
            var fromAddress = new MailAddress(model.From, model.FromName);
            var toAddress = new MailAddress(model.To, model.ToName);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, model.FromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = model.Subject,
                Body = model.Content
            })
            {
                smtp.Send(message);
            }
        }

        public string SendFeedBack(SendMailModel model)
        {
            var fromAddress = new MailAddress(model.From, model.FromName);
            var toAddress = new MailAddress(model.To, model.ToName);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, model.FromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = model.Subject,
                Body = model.Content
            })
            {
                try
                {
                    smtp.Send(message);

                    return "ok";
                }
                catch (Exception ext)
                {
                    return ext.ToString();
                }

            }
        }

        public JsonResult Test()
        {
            return Json("test", JsonRequestBehavior.AllowGet);
        }

    }
}
