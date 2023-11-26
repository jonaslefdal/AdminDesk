using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using Google.Protobuf;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminDesk.Controllers
{
    public class ServiceOrderController : Controller

    {

        private readonly IServiceOrderRepository _serviceOrderRepository;

        private readonly ICustomerRepository _customerRepository;

        private readonly IReportRepository _reportRepository;

        private readonly UserManager<IdentityUser> _userManager;



        public ServiceOrderController(IServiceOrderRepository serviceOrderRepository, IReportRepository reportRepository, ICustomerRepository customerRepository, UserManager<IdentityUser> userManager)

        {
            _serviceOrderRepository = serviceOrderRepository;
            _reportRepository = reportRepository;
            _customerRepository = customerRepository;
            _userManager = userManager;

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



            }).ToList();
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult NyServiceOrdre()
        {
            var model = new ServiceOrderFullViewModel();
            {
                model.UpsertModel = new ServiceOrderViewModel
                {
                    // other properties
                    CreatedDate = DateTime.Today // set the CreatedDate to the current date
                };
            };

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
                        TotalWorkHours = serviceOrdre.TotalWorkHours
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
        public async Task<IActionResult> Post(ServiceOrderFullViewModel serviceordre)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            string currentUserId = currentUser?.Id;

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
            _customerRepository.Upsert(customerEntity);


            var entity = new ServiceOrder
            {
                Mechanic = serviceordre.UpsertModel.Mechanic,
                ServiceOrderId = serviceordre.UpsertModel.ServiceOrderId,

                CustomerId = customerEntity.CustomerId,

                SerialNumber = serviceordre.UpsertModel.SerialNumber,
                CreatedDate = serviceordre.UpsertModel.CreatedDate,
                Comment = serviceordre.UpsertModel.Comment,
                FutureMaintenance = serviceordre.UpsertModel.FutureMaintenance,
                CreatedById = currentUserId,
                OrderStatus = serviceordre.UpsertModel.OrderStatus,
                TotalWorkHours = serviceordre.UpsertModel.TotalWorkHours,

            };

            _serviceOrderRepository.Upsert(entity);


            return Index();
        }



        [HttpGet]
        public IActionResult ServiceOrderEdit(int id)
        {
            var serviceOrder = _serviceOrderRepository.Get(id);

            if (serviceOrder == null)
            {
                return NotFound();
            }

            var model = new ServiceOrderFullViewModel
            {
                UpsertModel = new ServiceOrderViewModel
                {
                    ServiceOrderId = serviceOrder.ServiceOrderId,
                    CustomerId = serviceOrder.CustomerId,
                    Mechanic = serviceOrder.Mechanic,
                    SerialNumber = serviceOrder.SerialNumber,
                    CreatedDate = serviceOrder.CreatedDate,
                    Comment = serviceOrder.Comment,
                    FutureMaintenance = serviceOrder.FutureMaintenance,
                    CreatedById = serviceOrder.CreatedById,
                    OrderStatus = serviceOrder.OrderStatus,
                    TotalWorkHours = serviceOrder.TotalWorkHours
                }
            };

            if (serviceOrder.Customer != null)
            {
                model.UpsertModel.Customer = new Customer
                {
                    CustomerId = serviceOrder.Customer.CustomerId,
                    CustomerFirstName = serviceOrder.Customer.CustomerFirstName,
                    CustomerLastName = serviceOrder.Customer.CustomerLastName,
                    CustomerEmail = serviceOrder.Customer.CustomerEmail,
                    CustomerStreet = serviceOrder.Customer.CustomerStreet,
                    CustomerCity = serviceOrder.Customer.CustomerCity,
                    CustomerZipcode = serviceOrder.Customer.CustomerZipcode,
                    CustomerPhoneNumber = serviceOrder.Customer.CustomerPhoneNumber,
                    CustomerComment = serviceOrder.Customer.CustomerComment
                };

            }

            return View("ServiceOrderEdit", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChange(ServiceOrderFullViewModel serviceordre)
        {
            if (ModelState.IsValid)
            {
                // Update the service order
                var serviceOrderEntity = new ServiceOrder
                {
                    ServiceOrderId = serviceordre.UpsertModel.ServiceOrderId,
                    Mechanic = serviceordre.UpsertModel.Mechanic,
                    CustomerId = serviceordre.UpsertModel.CustomerId,
                    SerialNumber = serviceordre.UpsertModel.SerialNumber,
                    CreatedDate = serviceordre.UpsertModel.CreatedDate,
                    Comment = serviceordre.UpsertModel.Comment,
                    FutureMaintenance = serviceordre.UpsertModel.FutureMaintenance,
                    CreatedById = serviceordre.UpsertModel.CreatedById,
                    OrderStatus = serviceordre.UpsertModel.OrderStatus,
                    TotalWorkHours = serviceordre.UpsertModel.TotalWorkHours,
                };

                _serviceOrderRepository.Upsert(serviceOrderEntity);

                // Retrieve the existing customer associated with the service order
                var existingCustomer = _customerRepository.Get(serviceordre.UpsertModel.Customer.CustomerId);

                if (existingCustomer != null)
                {
                    // Update the existing customer information
                    existingCustomer.CustomerFirstName = serviceordre.UpsertModel.Customer.CustomerFirstName;
                    existingCustomer.CustomerLastName = serviceordre.UpsertModel.Customer.CustomerLastName;
                    existingCustomer.CustomerEmail = serviceordre.UpsertModel.Customer.CustomerEmail;
                    existingCustomer.CustomerStreet = serviceordre.UpsertModel.Customer.CustomerStreet;
                    existingCustomer.CustomerCity = serviceordre.UpsertModel.Customer.CustomerCity;
                    existingCustomer.CustomerZipcode = serviceordre.UpsertModel.Customer.CustomerZipcode;
                    existingCustomer.CustomerPhoneNumber = serviceordre.UpsertModel.Customer.CustomerPhoneNumber;
                    existingCustomer.CustomerComment = serviceordre.UpsertModel.Customer.CustomerComment;

                    _customerRepository.Upsert(existingCustomer);
                }

                return Index();
            }

            // If ModelState is not valid, return to the edit view with validation errors
            return View("ServiceOrderEdit", serviceordre);
        }




    }
}
