
using System.ComponentModel.DataAnnotations;

namespace Laneta.Entities
{
    public class Entity<TKey>
    {
       [Key]
       public TKey ID { get; set; }
    }

    public class Entity : Entity<int>
    {
    }
}
