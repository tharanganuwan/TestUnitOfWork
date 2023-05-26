using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.DomainServices;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Core.ApplicationServices.Services
{
    public class GlobalUnitOfWorkService : IGlobalUnitOfWorkService
    {
        private readonly IGlobalUnitOfWorkRepository _globalUnit;

        public GlobalUnitOfWorkService(IGlobalUnitOfWorkRepository globalUnit)
        {
            _globalUnit = globalUnit;
        }

        public void CreateRequestResponseLogs(string description, User user, int result, string response)
        {
            try
            {
                _globalUnit.CreateRequestResponseLogs(description, user, result, response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
