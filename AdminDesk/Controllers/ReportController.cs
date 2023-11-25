using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using Google.Protobuf;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static AdminDesk.Entities.Report;

namespace AdminDesk.Controllers
{
    public class ReportController : Controller

    {

        private readonly IReportRepository _reportRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public ReportController(IReportRepository reportRepository, UserManager<IdentityUser> userManager)
        {
            _reportRepository = reportRepository;
            _userManager = userManager;
        }


        [HttpGet]

        public IActionResult Index(int serviceOrderId)
        {
            var model = new List<ReportViewModel>
    {
        new ReportViewModel
        {
            // Set other properties as needed
            ServiceOrderId = serviceOrderId
        }
    };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> PostReport(ReportFullViewModel report)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            string currentUserId = currentUser?.Id;

            if (currentUserId == null)
            {
                // Handle the case where the user ID is null (e.g., throw an exception or provide a default value)
                throw new Exception("Current user ID is null.");
            }


            if (ModelState.IsValid)
            {
                // Assuming Upsert method requires a Report object
                var reportEntity = new Report
                {
                    // Map properties from the ViewModel
                    ServiceOrderId = report.UpsertModel.ServiceOrderId,
                    Mechanic = report.UpsertModel.Mechanic,
                    ServiceType = report.UpsertModel.ServiceType,
                    MechanicComment = report.UpsertModel.MechanicComment,
                    ServiceDescription = report.UpsertModel.ServiceDescription,
                    ReportWriteDate = report.UpsertModel.ReportWriteDate,
                    UserSign = currentUserId,
                    // ... other properties
                };

                _reportRepository.Upsert(reportEntity);

                // Redirect to the details page or any other page as needed
                return RedirectToAction("Spesific", "ServiceOrder", new { id = report.UpsertModel.ServiceOrderId });
            }

            else
            {
                // Log or debug to see the ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Return to the view with validation errors
                return View("Index", report);
            }

        }
    }
}