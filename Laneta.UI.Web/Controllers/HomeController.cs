namespace Laneta.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Laneta.EntityFramework;
    using Laneta.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly IServiceTicketRepository serviceTickets;
        private readonly IMessageRepository messageRepository;
        private readonly IAlertRepository alertRepository;
        private readonly IScheduleItemRepository scheduleItemRepository;

        public HomeController(
                              IServiceTicketRepository serviceTickets,
                              IMessageRepository messageRepository,
                              IAlertRepository alertRepository,
                              IScheduleItemRepository scheduleItemRepository)
        {
            this.serviceTickets = serviceTickets;
            this.messageRepository = messageRepository;
            this.alertRepository = alertRepository;
            this.scheduleItemRepository = scheduleItemRepository;
        }

        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                ScheduleItems = this.scheduleItemRepository.All,
                Messages = this.messageRepository.All,
                Alerts = this.alertRepository.All,
                Tickets = this.serviceTickets.AllIncluding(serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo),
            };

            return View(viewModel);
        }
    }
}
