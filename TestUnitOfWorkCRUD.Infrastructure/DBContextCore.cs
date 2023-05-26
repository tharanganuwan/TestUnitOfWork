using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Infrastructure
{
    public class DBContextCore : DbContext
    {
        public DBContextCore(DbContextOptions<DBContextCore> options) : base(options)
        {
        }
        public DbSet<User> tbl_User { get; set; }
    }
}
