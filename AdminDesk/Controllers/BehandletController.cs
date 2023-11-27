using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using Google.Protobuf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDesk.Controllers
{
    [Authorize(Policy = "RequireUserOrAdminRole")]
    public class BehandletController : Controller

    {

        private readonly IServiceOrderRepository _serviceOrderRepository;

        private readonly ICustomerRepository _customerRepository;

        private readonly IReportRepository _reportRepository;


        public BehandletController(IServiceOrderRepository serviceOrderRepository, IReportRepository reportRepository, ICustomerRepository customerRepository)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _reportRepository = reportRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ServiceOrderFullViewModel();
            model.ServiceOrderList = _serviceOrderRepository
                .GetAll()
                .Where(x => x.OrderStatus == "Fullført") 
                .Select(x => new ServiceOrderViewModel
                {
                    ServiceOrderId = x.ServiceOrderId,
                    Mechanic = x.Mechanic,
                    CustomerId = x.CustomerId,
                    SerialNumber = x.SerialNumber,
                    CreatedDate = x.CreatedDate,
                    Comment = x.Comment,
                    FutureMaintenance = x.FutureMaintenance,
                    CreatedById = x.CreatedById,
                    OrderStatus = x.OrderStatus,
                    TotalWorkHours = x.TotalWorkHours,

                    Customer = new Customer
                    {
                        // Map customer properties as needed
                        CustomerId = x.Customer.CustomerId,
                        CustomerFirstName = x.Customer.CustomerFirstName,
                        CustomerLastName = x.Customer.CustomerLastName,
                        CustomerEmail = x.Customer.CustomerEmail,
                        CustomerStreet = x.Customer.CustomerStreet,
                        CustomerCity = x.Customer.CustomerCity,
                        CustomerZipcode = x.Customer.CustomerZipcode,
                        CustomerPhoneNumber = x.Customer.CustomerPhoneNumber,
                        CustomerComment = x.Customer.CustomerComment,
                    }
                })
                .ToList();

            return View("Index", model);
        }


    }
}
