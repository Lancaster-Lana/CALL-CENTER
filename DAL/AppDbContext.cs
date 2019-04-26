namespace Laneta.DAL.Data
{
    using Laneta.DAL.Entity;
    using System.Data.Entity;

    public class AppDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ServiceTicket> ServiceTickets { get; set; }

        public DbSet<ServiceLogEntry> ServiceLogEntries { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Alert> Alerts { get; set; }

        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Laneta.DAL.Entity.LanetaWebContext>());
        public AppDBContext()
            : base("CallCenter-Express")
        {
        }

        public AppDBContext(string connectionString)
            : base(connectionString)
        {
        }

    }
}