using System.Data.Entity;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Context
{
    public class TPGPContext : DbContext
    {
        public TPGPContext() : base()
        {
            Database.SetInitializer<TPGPContext>(new TPGPDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasMany(p => p.Roles).WithMany(p => p.Permissions)
                                             .Map(pr =>
                                             {
                                                 pr.MapLeftKey("permission_id");
                                                 pr.MapRightKey("role_id");
                                                 pr.ToTable("link_permissions_roles");
                                             });
            modelBuilder.Entity<User>().HasRequired<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey<long>(u => u.RoleId);
        }
    }
}