namespace Laneta.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Laneta.Web.ViewModels;
    using Laneta.Entities;
    using Laneta.EntityFramework;

    [Route("[controller]/[action]")]
    public class ServiceTicketsController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IServiceTicketRepository serviceTicketRepository;
        private readonly IServiceLogEntryRepository serviceLogEntryRepository;
        private readonly IScheduleItemRepository scheduleItemRepository;

        public ServiceTicketsController(
                                        ICustomerRepository customerRepository,
                                        IEmployeeRepository employeeRepository,
                                        IServiceTicketRepository serviceTicketRepository,
                                        IServiceLogEntryRepository serviceLogEntryRepository,
                                        IScheduleItemRepository scheduleItemRepository)
        {
            this.customerRepository = customerRepository;
            this.employeeRepository = employeeRepository;
            this.serviceTicketRepository = serviceTicketRepository;
            this.serviceLogEntryRepository = serviceLogEntryRepository;
            this.scheduleItemRepository = scheduleItemRepository;
        }

        public ViewResult Index()
        {
            return View(this.serviceTicketRepository.AllIncluding(serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo));
        }

        public ViewResult Details(int id)
        {
            return View(this.serviceTicketRepository.FindIncluding(id, serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo));
        }

        /// <summary>
        /// Assign ticket id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewResult Assign(int id)
        {
            var viewModel = new AssignViewModel
            {
                ServiceTicket = this.serviceTicketRepository.FindIncluding(id, serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo),
                AvailableEmployees = this.employeeRepository.All,
                ScheduleItems = this.scheduleItemRepository.AllIncluding(p => p.ServiceTicket)
            };

            return View(viewModel);
        }

        /// <summary>
        /// Show scheduler for the employeeId
        /// </summary>
        /// <param name="serviceTicketId"></param>
        /// <param name="employeeId"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public ViewResult Schedule(int serviceTicketId, int employeeId, float startTime)
        {
            var viewModel = new ScheduleViewModel
            {
                ServiceTicket = this.serviceTicketRepository.FindIncluding(serviceTicketId, serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo),
                Employee = this.employeeRepository.Find(employeeId),
                ScheduleItems = this.scheduleItemRepository.AllIncluding(e => e.ServiceTicket).Where(item => item.EmployeeID == employeeId),
            };

            ViewBag.StartTime = startTime;

            return View(viewModel);
        }

        /// <summary>
        /// Reschedule tickets for employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="tickets"></param>
        /// <returns></returns>
        [HttpPut]//("{employeeId}")]
        [Route("{employeeId}")]
        public ActionResult Reschedule(int employeeId, [FromBody]ScheduleItem[] tasks)
        {
            //this.scheduleItemRepository.All.Where(e => e.ServiceTicketID == serviceTicketId)
            //                               .ToList()
            //                               .ForEach(e => this.scheduleItemRepository.Delete(e.ID));

            //TODO: save resceduled tickets in DB
            foreach (var task in tasks)
            {                
                //scheduleItem.AssignedOn = DateTime.Now;
                task.EmployeeID = employeeId; //scheduleItem.ServiceTicket.AssignedToID = employeeId;

                //Calculate work hours for task
                var startDT = task.Start;
                var startAtTime = startDT.TimeOfDay;//DateTime.Parse(startDT, "ddd dd MMM h:mm tt yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var endDT = task.End;
                var endAtTime = endDT?.TimeOfDay;//DateTime.ParseExact(endDT, "ddd dd MMM h:mm tt yyyy", System.Globalization.CultureInfo.InvariantCulture);

                double? durationHours = endAtTime?.Subtract(startAtTime).Hours;

                //if task for one day
                if (startDT.Date < endDT?.Date && endDT != null)
                {
                    var days = endDT?.Date.Subtract(startDT.Date).TotalDays;
                    //add total days
                    durationHours += days * 8;//endAt.Subtract(startAt).Hours;
                }

                task.WorkHours = (int)durationHours;//scheduleItem.WorkHours;
                //scheduleItem.ServiceTicket.Status = Status.Assigned;

                this.scheduleItemRepository.InsertOrUpdate(task);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Assign and schedule new ticket 
        /// </summary>
        /// <param name="serviceTicketId"></param>
        /// <param name="employeeId"></param>
        /// <param name="startDT"></param>
        /// <param name="endDT"></param>
        /// <returns></returns>
        [HttpPost]
        //[ActionName("Schedule")]
        public ActionResult Schedule(int serviceTicketId, int employeeId, DateTime startDT, DateTime endDT)
        {
            this.scheduleItemRepository.All.Where(e => e.ServiceTicketID == serviceTicketId)
                                           .ToList()
                                           .ForEach(e => this.scheduleItemRepository.Delete(e.ID));

            var serviceTicket = this.serviceTicketRepository.Find(serviceTicketId);
            //var time = string.Format("Mon 16 May {0:d2}:{1:d2} {2} 2019", ((int)startTime > 12 ? (int)startTime - 12 : (int)startTime) / 1, startTime % 1 == 0.5 ? 30 : 0, startTime < 12 ? "AM" : "PM");
            //var time = DateTime.Parse(startDT);
            var startAtTime = startDT.TimeOfDay;//DateTime.Parse(startDT, "ddd dd MMM h:mm tt yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var endAtTime = endDT.TimeOfDay;//DateTime.ParseExact(endDT, "ddd dd MMM h:mm tt yyyy", System.Globalization.CultureInfo.InvariantCulture);

            double durationHours = endAtTime.Subtract(startAtTime).Hours;

            //if task for one day
            if (startDT.Date < endDT.Date)
            {               
                var days = endDT.Date.Subtract(startDT.Date).TotalDays;
                //add total days
                durationHours += days * 8;//endAt.Subtract(startAt).Hours;
            }

            var scheduleItem = new ScheduleItem {
                EmployeeID = employeeId,
                ServiceTicketID = serviceTicketId,
                Start = startDT, 
                //End = endAt,
                WorkHours = (int)durationHours, //duration in hours
                //WorkMinutes = duration.Minutes,
                AssignedOn = DateTime.Now
            };

            this.scheduleItemRepository.InsertOrUpdate(scheduleItem);

            serviceTicket.AssignedToID = employeeId;
            serviceTicket.Status = Status.Assigned;

            this.serviceTicketRepository.Save();
            this.scheduleItemRepository.Save();

            return RedirectToAction("Details", new { serviceTicket.ID });
        }

        public ActionResult Create()
        {
            ViewBag.PossibleCustomers = this.customerRepository.All;
            ViewBag.PossibleCreatedBies = this.employeeRepository.All;
            ViewBag.PossibleAssignedToes = this.employeeRepository.All;
            ViewBag.PossibleEscalationLevels = new Dictionary<string, string> { { "1", "Level 1" }, { "2", "Level 2" }, { "3", "Level 3" } };

            var newTicket = new ServiceTicket
            {
                CreatedBy = this.employeeRepository.All.Where(e => e.Identity == "NORTHAMERICA\\drobbins").FirstOrDefault(),
            };

            return View(newTicket);
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Create(ServiceTicket serviceticket)
        {
            if (ModelState.IsValid)
            {
                serviceticket.Opened = DateTime.Now;

                var createdBy = this.employeeRepository.All.Where(e => e.Identity == "NORTHAMERICA\\drobbins").FirstOrDefault();

                if (createdBy != null)
                    serviceticket.CreatedByID = createdBy.ID;

                var serviceLogEntry = new ServiceLogEntry
                {
                    ServiceTicket = serviceticket,
                    CreatedAt = DateTime.Now,
                    CreatedBy = serviceticket.CreatedBy,
                    CreatedByID = serviceticket.CreatedByID,
                    Description = "Created",
                };

                this.serviceLogEntryRepository.InsertOrUpdate(serviceLogEntry);
                this.serviceLogEntryRepository.Save();

                return RedirectToAction("Details", new { serviceticket.ID });
            }
            else
            {
                ViewBag.PossibleCustomers = this.customerRepository.All;
                ViewBag.PossibleCreatedBies = this.employeeRepository.All;
                ViewBag.PossibleAssignedToes = this.employeeRepository.All;
                ViewBag.PossibleEscalationLevels = new Dictionary<string, string> { { "1", "Level 1" }, { "2", "Level 2" }, { "3", "Level 3" } };

                var newTicket = new ServiceTicket
                {
                    CreatedBy = this.employeeRepository.All.Where(e => e.Identity == "NORTHAMERICA\\drobbins").FirstOrDefault(),
                };

                return View(newTicket);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleCustomers = this.customerRepository.All;
            ViewBag.PossibleCreatedBies = this.employeeRepository.All;
            ViewBag.PossibleAssignedToes = this.employeeRepository.All;
            ViewBag.PossibleEscalationLevels = new Dictionary<string, string> { { "1", "Level 1" }, { "2", "Level 2" }, { "3", "Level 3" } };

            return View(this.serviceTicketRepository.FindIncluding(id, serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo));
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Edit(ServiceTicket serviceticket)
        {
            if (ModelState.IsValid)
            {
                this.serviceTicketRepository.InsertOrUpdate(serviceticket);
                this.serviceTicketRepository.Save();
                return RedirectToAction("Details", new { id = serviceticket.ID });
            }

            this.ViewBag.PossibleCustomers = this.customerRepository.All;
            this.ViewBag.PossibleCreatedBies = this.employeeRepository.All;
            this.ViewBag.PossibleAssignedToes = this.employeeRepository.All;
            this.ViewBag.PossibleEscalationLevels = new Dictionary<string, string> { { "1", "Level 1" }, { "2", "Level 2" }, { "3", "Level 3" } };

            return this.View();
        }

        public ActionResult Delete(int id)
        {
            return View(this.serviceTicketRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.serviceTicketRepository.Delete(id);
            this.serviceTicketRepository.Save();

            return RedirectToAction("Index");
        }

        public JsonResult GetLogEntries(int id)
        {
            var result = this.serviceLogEntryRepository.All.Where(entry => entry.ServiceTicketID == id);
            //return Json(new { entries = result.ToList() }, JsonRequestBehavior.AllowGet);
            return Json(new { entries = result.ToList() });
        }
    }
}