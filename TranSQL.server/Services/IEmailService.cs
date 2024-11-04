using TranSQL.shared.models;

namespace TranSQL.server.Services
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string subject, string body, List<string> recipients);

        //Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = false);
        Task SendEmailAsync(List<string> toEmails, string subject, string body, bool isHtml = false);

        //Task NotifyLogisticsOnNewRequest(Colaborador requester);
        //Task NotifyRequesterOnRejection(Colaborador requester);
        //Task NotifyOnApproval(Colaborador requester, List<string> securityEmails);
    }
}
