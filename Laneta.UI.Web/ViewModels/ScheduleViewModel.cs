namespace Laneta.Web.ViewModels
{
    using System.Linq;
    using Laneta.Entities;

    public class ScheduleViewModel
    {
        public ServiceTicket ServiceTicket { get; set; }

        public Employee Employee { get; set; }

        public IQueryable<ScheduleItem> ScheduleItems { get; set; }
    }
}