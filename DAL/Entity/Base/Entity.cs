
namespace Laneta.DAL.Entity
{
    public class Entity<TKey>
    {
       public TKey ID { get; set; }
    }

    public class Entity : Entity<int>
    {
    }
}
