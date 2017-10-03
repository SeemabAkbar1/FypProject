using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;

namespace PUCIT.AIMRL.WebAppName.DAL
{
    public class PRMDataContext : DbContext
    {
        private static readonly string ConnectionString = DatabaseHelper.Instance.MainDBConnectionString;

        public DbSet<SampleStudent> SampleStudents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsMapping> PermissionsMapping { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<EmailRequest> EmailRequests { get; set; }

        public PRMDataContext()
            : base(ConnectionString)
        {
            // We'll eager load entities whenever required.
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 3000;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}



