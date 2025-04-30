using System.Data.SqlClient;
using CRUDify_API.Entities;
using CRUDify_API.Repositories.Abstract;
using Dapper;

namespace CRUDify_API.Repositories.Concrete
{
    public class LocationRepository : ILocationRepository
    {
        private readonly string _connectionString;

        public LocationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Location>("dbo.sp_GetAllLocation", commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
