using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.DomainServices;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContextCore _context;
        public UserRepository(DBContextCore context)
        {
            _context = context;
        }
        public async Task<int>  CheckGlobalUserExistence(string email)
        {
            try
            {
                var emailParam = new SqlParameter("@Email", email);
                var outputParm = new SqlParameter("@Output", SqlDbType.Int) { Direction = ParameterDirection.Output };
                await _context.Database.ExecuteSqlRawAsync("EXEC check_user_existence @Email, @Output OUTPUT", emailParam,outputParm);

                var result = outputParm.Value;

                if (result == DBNull.Value) // Check if the result is null
                {
                    throw new InvalidOperationException("Repository level returns a null value");
                }
                var returnValue = Convert.ToInt32(result);
                return returnValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> CreateCashierAcc(User user)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@UserID", user.UserId),
                    new SqlParameter("@FirstName", user.FirstName),
                    new SqlParameter("@MiddleName", user.MiddleName),
                    new SqlParameter("@LastName", user.LastName),
                    new SqlParameter("@Email", user.Email),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@Status", user.Status),
                    new SqlParameter("@StartDate", user.StartDate),
                    new SqlParameter("@UserType", user.UserType),
                    new SqlParameter("@OutStatus",SqlDbType.Int){ Direction = ParameterDirection.Output }
            };

                const string sql = "EXEC save_user " +
                                    "@UserId," +
                                    "@FirstName," +
                                    "@MiddleName," +
                                    "@LastName," +
                                    "@Email," +
                                    "@Password," +
                                    "@Status," +
                                    "@StartDate," +
                                    "@UserType,"+
                                    "@OutStatus OUTPUT";

                await _context.Database.ExecuteSqlRawAsync(sql, parameters);

                var status = parameters[9].Value.ToString();

                return int.Parse(status ??
                                 throw new InvalidOperationException("Repository level returns a null value"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
