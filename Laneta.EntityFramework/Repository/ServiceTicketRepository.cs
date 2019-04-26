namespace Laneta.EntityFramework
{
    using Laneta.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IServiceTicketRepository : IRepository<ServiceTicket>
    {
        IQueryable<ServiceTicket> AllForReport(params Expression<Func<ServiceTicket, object>>[] includeProperties);
    }

    public class ServiceTicketRepository : Repository<ServiceTicket>, IServiceTicketRepository
    {
        private AppDBContext _appContext => (AppDBContext)_context;

        public ServiceTicketRepository(AppDBContext context) : base(context)
        {
        }

        public IQueryable<ServiceTicket> AllForReport(params Expression<Func<ServiceTicket, object>>[] includeProperties)
        {
            //var warehouseContext = new AppDBContext("CallCenter-DataWarehouse");
            IQueryable<ServiceTicket> query = _appContext.ServiceTickets;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
    }
}