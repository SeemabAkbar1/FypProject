using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace PUCIT.AIMRL.Common
{
    public class EmailHandler
    {
        private String _SMTPHost;
        private String _defaultCCIds;
        private String _defaultBCCIds;

        public String SMTPHost
        {
            get
            {
                return _SMTPHost;
            }
        }

        public EmailHandler(String pSMTPHost)
        {
            this._SMTPHost = pSMTPHost;
        }
        public EmailHandler(String pSMTPHost, String defaultCCIds, String defaultBCCIds)
        {
            this._SMTPHost = pSMTPHost;
            this._defaultCCIds = defaultCCIds;
            this._defaultBCCIds = defaultBCCIds;
        }
        public static Boolean SendEmail(String toEmailAddress, String subject, String body)
        {
            try
            {
                String fromDisplayEmail = "pucitaimrl@gmail.com";
                String fromPassword = "PUCIT123";

                String fromDisplayName = "Student Request Portal"; //This can be any text
                MailAddress fromAddress = new MailAddress(fromDisplayEmail, fromDisplayName);

                MailAddress toAddress = new MailAddress(toEmailAddress);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {

                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
    //public Boolean SendEmail(EmailMessageParam emailMessage)
    //    {
    //        try
    //        {
    //            if (emailMessage == null)
    //                throw new Exception("Message object can't be null");

    //            if (String.IsNullOrEmpty(emailMessage.ToIDs))
    //                throw new Exception("To IDs can't be empty");

    //            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

    //            mail.IsBodyHtml = emailMessage.IsBodyHTML;
    //            mail.Subject = emailMessage.Subject;

    //            mail.From = new MailAddress(emailMessage.FromID, emailMessage.FromName);

    //            var tos = emailMessage.ToIDs.Split(";".ToCharArray());

    //            foreach (var to in tos)
    //            {
    //                MailAddress toadd = new MailAddress(to);
    //                mail.To.Add(toadd);
    //            }

    //            /* CC Email Ids */
    //            if (!String.IsNullOrEmpty(emailMessage.CCIds))
    //            {
    //                var ids = emailMessage.CCIds.Split(";".ToCharArray());

    //                foreach (var cc in ids)
    //                {
    //                    MailAddress ccadd = new MailAddress(cc);
    //                    mail.CC.Add(ccadd);
    //                }
    //            }
    //            else if (!String.IsNullOrEmpty(this._defaultCCIds))
    //            {
    //                var ids = this._defaultCCIds.Split(";".ToCharArray());

    //                foreach (var cc in ids)
    //                {
    //                    MailAddress ccadd = new MailAddress(cc);
    //                    mail.CC.Add(ccadd);
    //                }
    //            }



    //            /* BCC Email Ids */
    //            if (!String.IsNullOrEmpty(emailMessage.BCCIds))
    //            {
    //                var ids = emailMessage.BCCIds.Split(";".ToCharArray());

    //                foreach (var bcc in ids)
    //                {
    //                    MailAddress bccadd = new MailAddress(bcc);
    //                    mail.Bcc.Add(bccadd);
    //                }
    //            }
    //            else if (!String.IsNullOrEmpty(this._defaultBCCIds))
    //            {
    //                var ids = this._defaultBCCIds.Split(";".ToCharArray());

    //                foreach (var bcc in ids)
    //                {
    //                    MailAddress bccadd = new MailAddress(bcc);
    //                    mail.Bcc.Add(bccadd);
    //                }
    //            }

    //            if (emailMessage.AlternateView != null)
    //                mail.AlternateViews.Add(emailMessage.AlternateView);


    //            if (emailMessage.Attachments != null)
    //            {
    //                foreach (var atch in emailMessage.Attachments)
    //                {
    //                    mail.Attachments.Add(atch);
    //                }
    //            }

    //            SmtpClient sc = new SmtpClient(this.SMTPHost);
    //            sc.Send(mail);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("PUCIT.AIMRL.Common.SendEmail()", ex);
    //        }

    //        return true;
    //    }
        

    //    /*
    //    To display images in an email, images are embeded as 'LinkedResource'
    //    First parameter of dictionary is 'ImagePath' & Second Parameter is tag string where resource will be replaced
    //     */
    //    public static List<LinkedResource> GetLinkedResourcesForImages(ref String pBodyHtml, Dictionary<String, String> imagePaths_Tags)
    //    {
    //        List<LinkedResource> linkedResources = new List<LinkedResource>();
    //        foreach (var item in imagePaths_Tags)
    //        {
    //            var uniqueId = Guid.NewGuid().ToString();
    //            LinkedResource logo = new LinkedResource(item.Value);
    //            logo.ContentId = uniqueId;

    //            pBodyHtml = pBodyHtml.Replace(item.Key, "cid:" + uniqueId);

    //            linkedResources.Add(logo);
    //        }

    //        return linkedResources;
    //    }

    //    public static AlternateView GetLogoAlternateView(String pBodyHtml, List<LinkedResource> linkedResources)
    //    {

    //        AlternateView av1 = AlternateView.CreateAlternateViewFromString(pBodyHtml, null, MediaTypeNames.Text.Html);
    //        foreach (var rsrc in linkedResources)
    //        {
    //            av1.LinkedResources.Add(rsrc);
    //        }
    //        return av1;

    //    }

   // }

    public class EmailMessageParam
    {
        public String FromID { get; set; }
        public String FromName { get; set; }
        public String ToIDs { get; set; }
        public String Subject { get; set; }

        public String Body { get; set; }

        public String CCIds { get; set; }

        public String BCCIds { get; set; }

        public List<Attachment> Attachments { get; set; }

        public Boolean IsBodyHTML { get; set; }

        public AlternateView AlternateView { get; set; }

        public EmailMessageParam()
        {
            this.IsBodyHTML = false;
        }

    }
}
