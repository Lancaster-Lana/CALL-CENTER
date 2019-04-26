namespace Laneta.EntityFramework
{
    using Laneta.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IScheduleItemRepository : IRepository<ScheduleItem>
    {
 
    }

    public class ScheduleItemRepository : Repository<ScheduleItem>, IScheduleItemRepository
    {
        private AppDBContext _appContext => (AppDBContext)_context;

        public ScheduleItemRepository(AppDBContext context) : base(context)
        {
        }
    }
}