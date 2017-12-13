using System.Data.Entity;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Context
{
    public class TPGPContext : DbContext
    {
        public TPGPContext() : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Contract> Contracts { get; set; }
    }
}