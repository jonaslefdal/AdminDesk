
using AdminDesk.Entities;
using AdminDesk.Models.CheckList;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using Google.Protobuf;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static AdminDesk.Entities.CheckList;

namespace AdminDesk.Controllers
{
    public class CheckListController : Controller

    {

        private readonly ICheckListRepository _checkListRepository;

        public CheckListController(ICheckListRepository checkListRepository)
        {
            _checkListRepository = checkListRepository;

        }


        [HttpGet]
        public IActionResult Index(int id)
        {
            // Fetch existing checklist for the given ServiceOrderId
            var existingChecklist = _checkListRepository.GetByServiceOrderId(id);


            if (existingChecklist == null)
            {
                var model = new CheckListFullViewModel
                {
                    UpsertModel = new CheckListViewModel
                    {
                        ServiceOrderId = id
                    }
                };

                return View("CheckList", model);
            }

            var checklistEntity = new CheckListFullViewModel
            {
                UpsertModel = new CheckListViewModel
                {
                    ServiceOrderId = existingChecklist.ServiceOrderId,
                    Avd1Sp1 = existingChecklist.Avd1Sp1,
                    Avd1Sp2 = existingChecklist.Avd1Sp2,
                    Avd1Sp3 = existingChecklist.Avd1Sp3,
                    Avd1Sp4 = existingChecklist.Avd1Sp4,
                    Avd1Sp5 = existingChecklist.Avd1Sp5,
                    Avd1Sp6 = existingChecklist.Avd1Sp6,
                    Avd1Sp7 = existingChecklist.Avd1Sp7,
                    Avd1Sp8 = existingChecklist.Avd1Sp8,
                    Avd2Sp1 = existingChecklist.Avd2Sp1,
                    Avd2Sp2 = existingChecklist.Avd2Sp2,
                    Avd2Sp3 = existingChecklist.Avd2Sp3,
                    Avd2Sp4 = existingChecklist.Avd2Sp4,
                    Avd2Sp5 = existingChecklist.Avd2Sp5,
                    Avd2Sp6 = existingChecklist.Avd2Sp6,
                    Avd2Sp7 = existingChecklist.Avd2Sp7,
                    Avd3Sp1 = existingChecklist.Avd3Sp1,
                    Avd3Sp2 = existingChecklist.Avd3Sp2,
                    Avd3Sp3 = existingChecklist.Avd3Sp3,
                    Avd4Sp1 = existingChecklist.Avd4Sp1,
                    Avd5Sp1 = existingChecklist.Avd5Sp1,
                    Avd5Sp2 = existingChecklist.Avd5Sp2,
                    Avd5Sp3 = existingChecklist.Avd5Sp3,
                }
            };


            return View("CheckList", checklistEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult PostCheckList(CheckListFullViewModel checklist)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return to the view with validation errors
                return View("CheckList", checklist);
            }

            var checklistEntity = new CheckList
            {
                // Map properties from the ViewModel

                ServiceOrderId = checklist.UpsertModel.ServiceOrderId,
                Avd1Sp1 = checklist.UpsertModel.Avd1Sp1,
                Avd1Sp2 = checklist.UpsertModel.Avd1Sp2,
                Avd1Sp3 = checklist.UpsertModel.Avd1Sp3,
                Avd1Sp4 = checklist.UpsertModel.Avd1Sp4,
                Avd1Sp5 = checklist.UpsertModel.Avd1Sp5,
                Avd1Sp6 = checklist.UpsertModel.Avd1Sp6,
                Avd1Sp7 = checklist.UpsertModel.Avd1Sp7,
                Avd1Sp8 = checklist.UpsertModel.Avd1Sp8,

                Avd2Sp1 = checklist.UpsertModel.Avd2Sp1,
                Avd2Sp2 = checklist.UpsertModel.Avd2Sp2,
                Avd2Sp3 = checklist.UpsertModel.Avd2Sp3,
                Avd2Sp4 = checklist.UpsertModel.Avd2Sp4,
                Avd2Sp5 = checklist.UpsertModel.Avd2Sp5,
                Avd2Sp6 = checklist.UpsertModel.Avd2Sp6,
                Avd2Sp7 = checklist.UpsertModel.Avd2Sp7,

                Avd3Sp1 = checklist.UpsertModel.Avd3Sp1,
                Avd3Sp2 = checklist.UpsertModel.Avd3Sp2,
                Avd3Sp3 = checklist.UpsertModel.Avd3Sp3,
                Avd4Sp1 = checklist.UpsertModel.Avd4Sp1,
                Avd5Sp1 = checklist.UpsertModel.Avd5Sp1,
                Avd5Sp2 = checklist.UpsertModel.Avd5Sp2,
                Avd5Sp3 = checklist.UpsertModel.Avd5Sp3,

            };

            _checkListRepository.Upsert(checklistEntity);

            // Redirect to the details page or any other page as needed
            return RedirectToAction("Spesific", "ServiceOrder", new { id = checklist.UpsertModel.ServiceOrderId });
        }


    }



}



