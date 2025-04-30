using System.Data;
using System.Data.SqlClient;
using CRUDify_API.Entities;
using CRUDify_API.Entities.DTOs;
using CRUDify_API.Repositories.Abstract;
using Dapper;

namespace CRUDify_API.Repositories.Concrete
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;

        public LogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<LogDTO> GetAllLogDetails()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<LogDTO>("dbo.sp_GetAllLogDTO", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Log> GetAllLogs()
        {
            throw new NotImplementedException();
        }

        public async Task<(int? LogID, string Message)> LogUserActivity(Guid userId, string activity, string tagCode)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userId, DbType.Guid);
                parameters.Add("@Activity", activity, DbType.String);
                parameters.Add("@TagCode", tagCode, DbType.String);

                try
                {
                    var result = await conn.QueryFirstOrDefaultAsync<dynamic>(
                        "dbo.sp_LogUserActivity",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    if (result == null)
                        return (null, "Unexpected error occurred");

                    int? logId = result.LogID;
                    string message = result.Message;

                    return (logId, message);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database error: {ex.Message}");
                }
            }
        }

    }
}
