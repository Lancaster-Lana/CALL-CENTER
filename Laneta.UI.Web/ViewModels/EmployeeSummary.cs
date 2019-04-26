using Laneta.Entities;

namespace Laneta.Web.ViewModels
{
    public class EmployeeSummary
    {
        public Employee Employee { get; set; }

        public Customer CurrentCustomer { get; set; }

        public int AssignedTickets { get; set; }
    }
}