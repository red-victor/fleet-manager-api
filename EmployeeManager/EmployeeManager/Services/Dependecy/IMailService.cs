using EmployeeManager.Models;
using System.Threading.Tasks;

namespace EmployeeManager.Services.Dependecy
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendResetPassEmailAsync(ResetPasswordMailRequest resetPasswordMailRequest);
    }
}
