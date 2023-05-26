using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Core.ApplicationServices
{
    public interface IUserService : IDisposable
    {
        Task<int> CreateUser(User user);
    }
}
