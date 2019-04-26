namespace Laneta.EntityFramework
{
    using Laneta.Entities;

    public interface IEmployeeRepository : IRepository<Employee>
    {
        
    }

    public class EmployeeRepository : Repository<Employee>,  IEmployeeRepository
    {
        private AppDBContext _appContext => (AppDBContext)_context;

        public EmployeeRepository(AppDBContext context) : base(context)
        {
        }
    }
}