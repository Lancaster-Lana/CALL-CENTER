namespace Laneta.Web.ViewModels
{
    using Laneta.Entities;
    using System.Collections.Generic;
    
    public class AssignViewModel
    {
        public ServiceTicket ServiceTicket { get; set; }

        public IEnumerable<Employee> AvailableEmployees { get; set; }

        public IEnumerable<ScheduleItem> ScheduleItems { get; set; }
    }
}