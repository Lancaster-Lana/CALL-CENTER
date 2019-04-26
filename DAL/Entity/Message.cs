namespace Laneta.DAL.Entity
{
    using System;

    public class Message : Entity
    {
        public DateTime Sent { get; set; }

        public string Description { get; set; }
    }
}