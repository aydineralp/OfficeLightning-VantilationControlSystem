using CRUDify_API.Entities;
using CRUDify_API.Entities.DTOs;

namespace CRUDify_API.Repositories.Abstract
{
    public interface ILogRepository
    {
        IEnumerable<LogDTO> GetAllLogDetails();
        IEnumerable<Log> GetAllLogs();
        Task<(int? LogID, string Message)> LogUserActivity(Guid userId, string activity, string tagCode);
    }
}
