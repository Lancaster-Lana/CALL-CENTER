namespace Laneta.Entities
{
    using System;

    public class ScheduleItem : Entity
    {
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        public int ServiceTicketID { get; set; }

        public ServiceTicket ServiceTicket { get; set; }

        public DateTime? AssignedOn { get; set; }

        public DateTime Start { get; set; }

        public int WorkHours { get; set; }
    }
}