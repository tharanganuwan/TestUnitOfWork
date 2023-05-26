using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.DomainServices;

namespace TestUnitOfWorkCRUD.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContextCore _context;
        private bool _disposed = false;

        public UnitOfWork(DBContextCore context)
        {
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _context != null)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
