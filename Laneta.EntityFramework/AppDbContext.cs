using Laneta.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Laneta.EntityFramework
{
    //public class DBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    //{
    //    /// <summary>
    //    /// TODO: from config 
    //    /// </summary>
    //    const string connectionString = //GlobalProperties.
    //        "Server=.;Database=CallCenterDB2;Trusted_Connection=True;MultipleActiveResultSets=true";

    //    public AppDBContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
    //        optionsBuilder.UseSqlServer(connectionString);

    //        return new AppDBContext(optionsBuilder.Options);
    //    }
    //}

    public class AppDBContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<ServiceTicket> ServiceTickets { get; set; }

        public virtual DbSet<ServiceLogEntry> ServiceLogEntries { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Alert> Alerts { get; set; }

        public virtual DbSet<ScheduleItem> ScheduleItems { get; set; }

        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Laneta.DAL.Entity.LanetaWebContext>());
        //public AppDBContext()
        //    : base("CallCenter-Express")
        //{
        //}

        //public AppDBContext(string connectionString)
        //    : base(connectionString)
        //{
        //}
 
        public AppDBContext(string connectionString) : this(GetOptions(connectionString))
        {
            //    this.Configuration.LazyLoadingEnabled = true;
            //    this.Configuration.ProxyCreationEnabled = false;
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        //If using Microsoft.EntityFrameworkCore
        public AppDBContext(DbContextOptions options)
            : base(options)
        {
            //options.FindExtension.
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GlobalProperties.DBConnection);
            }
        }*/


    }
}