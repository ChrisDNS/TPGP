using System.Data.Entity;
using TPGP.Models.Jobs;

namespace TPGP.Context
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
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<GeographicalZone> Zones { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired<Role>(u => u.Role).WithMany(r => r.Users);
            modelBuilder.Entity<Contract>().HasRequired<Portfolio>(c => c.Portfolio).WithMany(p => p.Contracts);
            modelBuilder.Entity<GeographicalZone>().HasOptional(g => g.Parent).WithMany().HasForeignKey<long?>(g => g.ParentId);
            modelBuilder.Entity<Permission>().HasMany(p => p.Roles).WithMany(p => p.Permissions)
                                             .Map(pr =>
                                             {
                                                 pr.MapLeftKey("permission_id");
                                                 pr.MapRightKey("role_id");
                                                 pr.ToTable("link_permissions_roles");
                                             });
            modelBuilder.Entity<Contract>().HasMany(c => c.Zones).WithMany(z => z.Contracts)
                                             .Map(cz =>
                                             {
                                                 cz.MapLeftKey("contract_id");
                                                 cz.MapRightKey("zone_id");
                                                 cz.ToTable("link_contracts_zones");
                                             });
        }
    }
}