using System.Net;
using Voluntr.Domain.Enumerators;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<HttpStatusCode> SendEmail(EmailTypeEnum emailType, string recipientName, string recipientEmail,
            Dictionary<string, string> data);

        Task<HttpStatusCode> SendEmailToManyRecipients(EmailTypeEnum emailType, string recipientName, List<string> recipientEmails,
           Dictionary<string, string> data);

        Task<HttpStatusCode> SendEmailWithAttachment(EmailTypeEnum emailType, string recipientName, string recipientEmail,
            Dictionary<string, string> data, string attachmentFile, string attachmentFileName, string attachmentFileType);
    }
}
