using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");

            //MailKit �̿�

        }

        public async static Task SendEmailConfirmationAsync(string email, string link)
        {
            await MailKitSender(email, "������丮 Ȩ������ ���� �̸��� ����",
                $"������丮 Ȩ������ ������ �Ϸ��Ϸ��� <b><a href='{HtmlEncoder.Default.Encode(link)}'>����</a></b>�� Ŭ���� �̸��� ������ �Ϸ� �ٶ��ϴ�.<br><br>�����մϴ�.");
        }

        /// <summary>
        /// �н����� �нǽ� �̸����� ���� ��й�ȣ �缳��
        /// </summary>
        /// <param name="email"></param>
        /// <param name="link"></param>
        public async static Task SendEmailForgotPasswordAsync(string email, string link)
        {
            await MailKitSender(email, "������丮 ��ȣ �缳��",
                $"������丮 Ȩ������ ��ȣ�� �缳�� �Ͻ÷��� <b><a href='{HtmlEncoder.Default.Encode(link)}'>����</a></b>�� Ŭ���� �̸��� ������ �Ϸ� �ٶ��ϴ�.<br><br>�����մϴ�.");
        }


        public async static Task MailKitSender(string email, string subject, string message)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("������丮", "devfactory7@naver.com"));
            msg.To.Add(new MailboxAddress(email));
            msg.Subject = subject;
            
            msg.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.naver.com", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("devfactory7", "dmsjj292513!");

                await client.SendAsync(msg);
                await client.DisconnectAsync(true);
            }
        }
    }
}
