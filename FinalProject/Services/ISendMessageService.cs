using Identity.DAL.Entities;

namespace FinalProject.Services;

public interface ISendMessageService
{
    Task SendReportAsync(User user);
}
