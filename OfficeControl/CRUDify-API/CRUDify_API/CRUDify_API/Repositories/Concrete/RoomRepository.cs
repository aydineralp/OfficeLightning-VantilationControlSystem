using System.Data;
using System.Data.SqlClient;
using CRUDify_API.Entities;
using CRUDify_API.Repositories.Abstract;
using Dapper;

namespace CRUDify_API.Repositories.Concrete
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString;
        public RoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Room>> GetRoomsByLocationName(string locationName)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@LocationName", locationName, DbType.String);
                return await conn.QueryAsync<Room>("dbo.sp_GetRoomByLocation", parameters,commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
