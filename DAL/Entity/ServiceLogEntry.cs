﻿namespace Laneta.DAL.Entity
{
    using System;

    public class ServiceLogEntry : Entity
    {
        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        public Employee CreatedBy { get; set; }

        public int? CreatedByID { get; set; }

        public ServiceTicket ServiceTicket { get; set; }

        public int ServiceTicketID { get; set; }
    }
}
