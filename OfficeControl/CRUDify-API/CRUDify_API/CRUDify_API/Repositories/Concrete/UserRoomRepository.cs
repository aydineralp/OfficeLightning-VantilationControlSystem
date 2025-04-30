using System.Data;
using System.Data.SqlClient;
using CRUDify_API.Entities;
using CRUDify_API.Entities.DTOs;
using CRUDify_API.Repositories.Abstract;
using Dapper;

namespace CRUDify_API.Repositories.Concrete
{
    public class UserRoomRepository : IUserRoomRepository
    {
        private readonly string _connectionString;
        public UserRoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public bool CheckUserRoomByCompositPK(Guid userID, Guid roomID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userID, DbType.Guid);
                parameters.Add("@RoomID", roomID, DbType.Guid);
                parameters.Add("@Exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                // Stored procedure çalıştırılıyor
                conn.Execute("dbo.sp_CheckUserRoomExists", parameters, commandType: CommandType.StoredProcedure);

                // Output parametresini alıyoruz
                return parameters.Get<bool>("@Exists");
            }
        }

        public IEnumerable<UserRoom> GetUserRoomByUserId(Guid userID)
        {
            throw new NotImplementedException();
        }
    }
}
