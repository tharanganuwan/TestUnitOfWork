using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.DomainServices;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Core.ApplicationServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepo;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepo)
        {
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
        }
        public async Task<int> CreateUser(User user)
        {
            using(_unitOfWork)
            {
                var transaction = _unitOfWork.BeginTransaction();
                try
                {
                    await transaction.CreateSavepointAsync("BeforeTransact");

                    var data = await _userRepo.CheckGlobalUserExistence(user.Email);
                    if (data == 9)
                    {
                        // there is no user.Checking user existence has thrown an exception. data = 9.
                        await transaction.RollbackToSavepointAsync("BeforeTransact");
                        return data;
                    }
                    if (data != 0)
                    {
                        // there is another user in the system already. overwrite data to 8 in this block
                        await transaction.RollbackToSavepointAsync("BeforeTransact");
                        return 8;
                    }
                    int result = await _userRepo.CreateCashierAcc(user);

                    if (result == 1)
                    {
                        const string division = "User Management";
                        const string operation = "New User Crated";
                        var description = "New user :- " + user.UserId + " created " ;

                        //Operation Log 
                        //_iGuw.InsertOperationsLogs(userId, centreCode, division, operation, description);

                        await _unitOfWork.SaveChanges();
                        await transaction.CommitAsync();

                        //return 1
                        return result;
                    }
                    //Something wrong...! Unsuccessful Transaction. return 7 here
                    await transaction.RollbackToSavepointAsync("BeforeTransact");
                    return 7;

                }
                catch (Exception e)
                {
                    await transaction.RollbackToSavepointAsync("BeforeTransact");
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
