namespace Laneta.EntityFramework
{
    using Laneta.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IAlertRepository : IRepository<Alert>
    {

    }

    public class AlertRepository : Repository<Alert>, IAlertRepository
    {
        public AlertRepository(AppDBContext context) : base(context)
        {
        }
    }
}