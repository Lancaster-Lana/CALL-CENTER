using Laneta.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laneta.EntityFramework
{
    public interface IMessageRepository : IRepository<Message>
    {
    }

    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private AppDBContext _appContext => (AppDBContext)_context;

        public MessageRepository(DbContext context) : base(context)
        {
        }
    }
}