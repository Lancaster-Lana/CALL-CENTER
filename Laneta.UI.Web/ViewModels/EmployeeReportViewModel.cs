namespace Laneta.Web.ViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Laneta.Entities;

    public class EmployeeReportViewModel : Controller
    {
        public IEnumerable<EmployeeSummary> Employees { get; set; }

        public string GetCustomerSummary(Customer customer)
        {
            return string.Format("{0}, {1}", customer?.FullName, customer?.Address?.City);
        }
    }
}
