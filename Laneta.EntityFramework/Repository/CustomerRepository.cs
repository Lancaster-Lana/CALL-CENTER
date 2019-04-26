namespace Laneta.EntityFramework
{
    using Laneta.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface ICustomerRepository : IRepository<Customer>
    {
    }

    public class CustomerRepository : Repository<Customer>,  ICustomerRepository
    {
        private AppDBContext _appContext => (AppDBContext)_context;

        public CustomerRepository(AppDBContext context) : base(context)
        {
        }
    }
}