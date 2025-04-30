using System.Data;
using System.Data.SqlClient;
using CRUDify_API.Entities;
using CRUDify_API.Entities.DTOs;
using CRUDify_API.Repositories.Abstract;
using Dapper;

namespace CRUDify_API.Repositories.Concrete
{
    public class TagRepository : ITagRepository
    {
        private readonly string _connectionString;
        public TagRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<TagDTO> GetAllTagDTOs()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<TagDTO>("dbo.sp_GetAllTagDTO", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> GetTagsByLocationName(string locationName)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@LocationName", locationName, DbType.String);
                return await conn.QueryAsync<Tag>("dbo.sp_GetTagByLocation", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tag>> GetTagsByRoomName(string roomName)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@RoomName", roomName, DbType.String);
                return await conn.QueryAsync<Tag>("dbo.sp_GetTagByRoom",parameters,commandType:System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
