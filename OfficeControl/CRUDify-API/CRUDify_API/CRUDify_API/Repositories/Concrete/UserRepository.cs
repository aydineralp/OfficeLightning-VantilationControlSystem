namespace CRUDify_API.Repositories.Concrete
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using CRUDify_API.Entities;
    using CRUDify_API.Repositories.Abstract;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<User> GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<User>("dbo.sp_GetAllUsers", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email,System.Data.DbType.String);
                return await conn.QueryFirstOrDefaultAsync<User>("dbo.sp_GetUserByEmail", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
