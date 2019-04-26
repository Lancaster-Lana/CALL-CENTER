namespace Laneta.EntityFramework
{
    using Laneta.Entities;
    using Laneta.EntityFramework;
    using Microsoft.EntityFrameworkCore;

    public interface IServiceLogEntryRepository : IRepository<ServiceLogEntry>
    {
 
    }

    public class ServiceLogEntryRepository : Repository<ServiceLogEntry>, IServiceLogEntryRepository
    {
        private AppDBContext _appContext => (AppDBContext)_context;

        public ServiceLogEntryRepository(AppDBContext context) : base(context)
        {
        }
    }
}