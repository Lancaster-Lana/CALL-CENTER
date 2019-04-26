namespace Laneta.Entities
{
    using System;

    public class Alert : Entity
    {
        public DateTime Created { get; set; }

        public string Description { get; set; }
    }
}