using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Core.DomainServices
{
    public interface IUserRepository
    {
        Task<int> CheckGlobalUserExistence(string email);
        Task<int> CreateCashierAcc(User user);
    }
}
