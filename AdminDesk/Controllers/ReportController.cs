using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Repositories;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using static AdminDesk.Entities.Report;

namespace AdminDesk.Controllers
{
    public class ReportController : Controller

    {

        private readonly IReportRepository _reportRepository;


        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }


        [HttpGet]
        public IActionResult Index(int id)
        {
            var reportsForServiceOrder = _reportRepository.GetAll().Where(r => r.ServiceOrderId == id).ToList();

            var model = new ReportFullViewModel
            {
                UpsertModel = new ReportViewModel(),
                ReportList = reportsForServiceOrder.Select(report => new ReportViewModel
                {
                    ReportId = report.ReportId,
                    ServiceOrderId = report.ServiceOrderId,
                    Mechanic = report.Mechanic,
                    ServiceType = report.ServiceType,
                    MechanicComment = report.MechanicComment,
                    ServiceDescription = report.ServiceDescription,
                    ReportWriteDate = report.ReportWriteDate,
                    UserSign = report.UserSign,
                }).ToList()
            };

            return View("Index", model.ReportList);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(ReportFullViewModel report)
        {
            var entity = new Report
            {
                ReportId = report.UpsertModel.ReportId,
                ServiceOrderId = report.UpsertModel.ServiceOrderId,
                // Set other properties as needed
            };

            _reportRepository.Upsert(entity);

            // Redirect to the Index action with the specified ServiceOrderId
            return RedirectToAction("Index", new { id = report.UpsertModel.ServiceOrderId });
        }

    }
}
