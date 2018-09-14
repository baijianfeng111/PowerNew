using System;
using System.Net.Mail;

namespace Rym.MonthWage.Bll
{
    public class SendMailManager
    {
        public static bool SendMail(MailInfo info)  //发送验证邮件
        {
            MailMessage message = new MailMessage();
            message.To.Add(info.MessageTo);
            message.From = info.MessageFrom;
            message.Subject = info.MessageSubject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = info.MessageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 
            SmtpClient sc = new SmtpClient();
            sc.EnableSsl = true;//是否SSL加密
            sc.UseDefaultCredentials = false;
            sc.Host = "smtp.qq.com"; //指定发送邮件的服务器地址或IP 
            sc.Port = 25; //指定发送邮件端口 
            //指定登录服务器的用户名和密码(注意：这里的密码是开通上面的pop3/smtp服务提供给你的授权密码，不是你的qq密码)
            sc.Credentials = new System.Net.NetworkCredential("806078508@qq.com", "jamuwmtceszubdjj");
            try
            {
                sc.Send(message); //发送邮件 
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            return true;
        }
        public class MailInfo
        {
            public MailInfo(MailAddress messageFrom, string messageTo, string messageSubject, string messageBody)
            {
                this.MessageFrom = messageFrom;
                this.MessageTo = messageTo;
                this.MessageBody = messageBody;
                this.MessageSubject = messageSubject;
            }
            public MailAddress MessageFrom { get; set; }
            public string MessageTo { get; set; }
            public string MessageSubject { get; set; }
            public string MessageBody { get; set; }
        }
    }
}
