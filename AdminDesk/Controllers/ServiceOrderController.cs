using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;

namespace AdminDesk.Controllers
{
    public class ServiceOrderController : Controller

    {

        private readonly IServiceOrderRepository _serviceOrderRepository;

        private readonly IReportRepository _reportRepository;


        public ServiceOrderController(IServiceOrderRepository serviceOrderRepository, IReportRepository reportRepository)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ServiceOrderFullViewModel();
            model.ServiceOrderList = _serviceOrderRepository.GetAll().Select(x => new ServiceOrderViewModel { ServiceOrderId = x.ServiceOrderId, Mechanic = x.Mechanic
                ,
                CustomerId = x.CustomerId
                ,
                SerialNumber = x.SerialNumber
                ,
                CreatedDate = x.CreatedDate
                ,
                Comment = x.Comment
                ,
                FutureMaintenance = x.FutureMaintenance
                ,
                CreatedById = x.CreatedById
                ,
                OrderStatus = x.OrderStatus
                ,
                ReserveDeler = x.ReserveDeler
                ,
                TotalArbeidsTimer = x.TotalArbeidsTimer,

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



            }).ToList();
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult NyServiceOrdre()
        {
            var model = new ServiceOrderFullViewModel();

            return View("NyServiceOrdre", model);
        }



        [HttpGet]
        public IActionResult Spesific(int id)
        {
            var serviceOrdre = _serviceOrderRepository.Get(id);
            

            if (serviceOrdre == null)
            {
                // Handle the case where the service order with the specified ID is not found
                return NotFound();
            }

            var reportsForServiceOrder = _reportRepository
        .GetAll()
        .Where(r => r.ServiceOrderId == id)
        .Select(r => new ReportViewModel
        {
            ReportId = r.ReportId,
            ServiceOrderId = r.ServiceOrderId,
            Mechanic = r.Mechanic,
            ServiceType = r.ServiceType,
            MechanicComment = r.MechanicComment,
            ServiceDescription = r.ServiceDescription,
            ReportWriteDate = r.ReportWriteDate,
            UserSign = r.UserSign
        })
        .ToList();

            var compositeModel = new CompositeViewModel
            {

                ServiceOrderModel = new ServiceOrderFullViewModel
                {
                    UpsertModel = new ServiceOrderViewModel
                    {
                        ServiceOrderId = serviceOrdre.ServiceOrderId,
                        CustomerId = serviceOrdre.CustomerId,
                        Mechanic = serviceOrdre.Mechanic,
                        SerialNumber = serviceOrdre.SerialNumber,
                        CreatedDate = serviceOrdre.CreatedDate,
                        Comment = serviceOrdre.Comment,
                        FutureMaintenance = serviceOrdre.FutureMaintenance,
                        CreatedById = serviceOrdre.CreatedById,
                        OrderStatus = serviceOrdre.OrderStatus,
                        ReserveDeler = serviceOrdre.ReserveDeler,
                        TotalArbeidsTimer = serviceOrdre.TotalArbeidsTimer
                    },
                    ServiceOrderList = new List<ServiceOrderViewModel>()
                },
                ReportModel = new ReportFullViewModel
                {
                    ReportList = reportsForServiceOrder
                   
                }
            };

            // Check if the Customer property is not null before accessing its properties
            if (serviceOrdre.Customer != null)
            {
                compositeModel.ServiceOrderModel.UpsertModel.Customer = new Customer
                {
                    CustomerId = serviceOrdre.Customer.CustomerId,
                    CustomerFirstName = serviceOrdre.Customer.CustomerFirstName,
                    CustomerLastName = serviceOrdre.Customer.CustomerLastName,
                    CustomerEmail = serviceOrdre.Customer.CustomerEmail,
                    CustomerStreet = serviceOrdre.Customer.CustomerStreet,
                    CustomerCity = serviceOrdre.Customer.CustomerCity,
                    CustomerZipcode = serviceOrdre.Customer.CustomerZipcode,
                    CustomerPhoneNumber = serviceOrdre.Customer.CustomerPhoneNumber,
                    CustomerComment = serviceOrdre.Customer.CustomerComment
                };
            }
            else
            {
                // Handle the case where the customer is null (optional)
                // You can set default values or handle it based on your requirements

            }

            return View("Spesific", compositeModel);
        
    }

        [HttpPost]
        [ValidateAntiForgeryToken]



        public IActionResult Post(ServiceOrderFullViewModel serviceordre)
        {
            var entity = new ServiceOrder
            {
                Mechanic = serviceordre.UpsertModel.Mechanic,
                ServiceOrderId = serviceordre.UpsertModel.ServiceOrderId,
                CustomerId = serviceordre.UpsertModel.CustomerId,
                SerialNumber = serviceordre.UpsertModel.SerialNumber,
                CreatedDate = serviceordre.UpsertModel.CreatedDate,
                Comment = serviceordre.UpsertModel.Comment,
                FutureMaintenance = serviceordre.UpsertModel.FutureMaintenance,
                CreatedById = serviceordre.UpsertModel.CreatedById,
                OrderStatus = serviceordre.UpsertModel.OrderStatus,
                ReserveDeler = serviceordre.UpsertModel.ReserveDeler,
                TotalArbeidsTimer = serviceordre.UpsertModel.TotalArbeidsTimer,

            };
            var customerEntity = new Customer
            {

                CustomerId = serviceordre.UpsertModel.Customer.CustomerId,
                CustomerFirstName = serviceordre.UpsertModel.Customer.CustomerFirstName,
                CustomerLastName = serviceordre.UpsertModel.Customer.CustomerLastName,
                CustomerEmail = serviceordre.UpsertModel.Customer.CustomerEmail,
                CustomerStreet = serviceordre.UpsertModel.Customer.CustomerStreet,
                CustomerCity = serviceordre.UpsertModel.Customer.CustomerCity,
                CustomerZipcode = serviceordre.UpsertModel.Customer.CustomerZipcode,
                CustomerPhoneNumber = serviceordre.UpsertModel.Customer.CustomerPhoneNumber,
                CustomerComment = serviceordre.UpsertModel.Customer.CustomerComment,

            };



            _serviceOrderRepository.Upsert(entity);


            return Index();
        }



    }
}
