using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.Services.Email
{
    public class EmailService(
        SendGridConfig configuration,
        IEmailRepository emailRepository
    ) : IEmailService
    {
        public async Task<HttpStatusCode> SendEmail(EmailTypeEnum emailType, string recipientName, string recipientEmail,
            Dictionary<string, string> data)
        {
            string templateId = await GetEmailTemplate(emailType);

            SendGridClient client = new(configuration.Key);
            EmailAddress from = new(configuration.Email, configuration.LabelName);
            EmailAddress to = new(recipientEmail, recipientName);
            SendGridMessage message = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);

            var sent = await client.SendEmailAsync(message);

            return sent.StatusCode;
        }

        public async Task<HttpStatusCode> SendEmailToManyRecipients(EmailTypeEnum emailType, string recipientName, List<string> recipientEmails,
            Dictionary<string, string> data)
        {
            string templateId = await GetEmailTemplate(emailType);

            SendGridClient client = new(configuration.Key);
            EmailAddress from = new(configuration.Email, configuration.LabelName);
            EmailAddress to = new(recipientEmails.FirstOrDefault(), recipientName);
            List<EmailAddress> tos = [];

            foreach (var email in recipientEmails.Skip(1))
                tos.Add(new EmailAddress(email, recipientName));

            SendGridMessage message = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);

            message.AddTos(tos);

            var sent = await client.SendEmailAsync(message);

            return sent.StatusCode;
        }

        public async Task<HttpStatusCode> SendEmailWithAttachment(EmailTypeEnum emailType, string recipientName, string recipientEmail,
            Dictionary<string, string> data, string attachmentFile, string attachmentFileName,
            string attachmentFileType)
        {
            string templateId = await GetEmailTemplate(emailType);

            SendGridClient client = new(configuration.Key);
            EmailAddress from = new(configuration.Email, configuration.LabelName);
            EmailAddress to = new(recipientEmail, recipientName);
            SendGridMessage message = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);

            Attachment attachment = new()
            {
                Content = attachmentFile,
                Type = attachmentFileType,
                Filename = attachmentFileName,
                Disposition = "attachment"
            };

            message.AddAttachment(attachment);

            var sent = await client.SendEmailAsync(message);

            return sent.StatusCode;
        }

        private async Task<string> GetEmailTemplate(EmailTypeEnum emailType)
        {
            var email = await emailRepository
                .GetFirstByExpressionAsync(x => x.Type == emailType.GetDescription());

            if (email != null)
                return email.TemplateId;

            return null;
        }
    }
}
