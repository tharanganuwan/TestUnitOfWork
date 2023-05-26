using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Core.DomainServices
{
    public interface IGlobalUnitOfWorkRepository
    {
        void CreateRequestResponseLogs(string description, User user, int result, string response);
    }
}
