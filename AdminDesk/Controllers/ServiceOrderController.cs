using AdminDesk.Entities;
using AdminDesk.Models.ServiceOrdrer;
using AdminDesk.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdminDesk.Controllers
{
    public class ServiceOrderController : Controller

    {

        private readonly IServiceOrdreRepository _serviceOrdreRepository;


        public ServiceOrderController(IServiceOrdreRepository serviceOrdreRepository)
        {
            _serviceOrdreRepository = serviceOrdreRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ServiceOrderFullViewModel();
            model.ServiceOrdreList = _serviceOrdreRepository.GetAll().Select(x => new ServiceOrderViewModel { ServiceOrderId = x.ServiceOrderId, CustomerName = x.CustomerName }).ToList();
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       


        public IActionResult Post(ServiceOrderFullViewModel serviceordre)
        {
            var entity = new ServiceOrdre
            {
                Mechanic = serviceordre.UpsertModel.Mechanic,
                SerialNumber = serviceordre.UpsertModel.SerialNumber,
                CreatedDate = serviceordre.UpsertModel.CreatedDate,
                ConsumedHours = serviceordre.UpsertModel.ConsumedHours,
                MechanicComment = serviceordre.UpsertModel.MechanicComment,
                CustomerComment = serviceordre.UpsertModel.CustomerComment,
                FutureMaintenance = serviceordre.UpsertModel.FutureMaintenance,
                ServiceOrderId = serviceordre.UpsertModel.ServiceOrderId,
                CustomerName = serviceordre.UpsertModel.CustomerName,
                CustomerEmail = serviceordre.UpsertModel.CustomerEmail,
                CustomerStreet = serviceordre.UpsertModel.CustomerStreet,
                CustomerCity = serviceordre.UpsertModel.CustomerCity,
                CustomerZipcode = serviceordre.UpsertModel.CustomerZipcode,
                CustomerTelephoneNumber = serviceordre.UpsertModel.CustomerTelephoneNumber,
                IsAdministrator = serviceordre.UpsertModel.IsAdministrator,
            };
            _serviceOrdreRepository.Upsert(entity);
            return Index();
        }
    }
}
