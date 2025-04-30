using System;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using CRUDify_API.Repositories.Abstract;
using System.Net;
using CRUDify_API.Entities.DTOs;
using System.Data.SqlClient;
using Dapper;
using CRUDify_API.Entities;
using System.Data;

namespace CRUDify_API.Repositories.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _domainName = "siskon.com";
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CheckUserByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@Exists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                conn.Query<User>("dbo.sp_CheckUserByEmail",parameters ,commandType: System.Data.CommandType.StoredProcedure);

                return parameters.Get<bool>("@Exists");
            }
        }

        public bool LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                Console.WriteLine($"Attempting Active Directory authentication for user: {email}");

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, _domainName))
                {
                    bool isValid = context.ValidateCredentials(email, password);

                    if (isValid)
                    {
                        Console.WriteLine($"Authentication successful for user: {email}");
                    }
                    else
                    {
                        Console.WriteLine($"Authentication failed for user: {email}");
                    }

                    return isValid;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception in AD Authentication: {ex.Message}");
                return false;
            }
        }
    }
}
