using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using AdminDesk.Controllers;
using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Repositories;

namespace AdminDesktest
{
    [TestFixture]
    public class ReportControllerTests
    {

        //Denne testen bekrefter at konstruktøren av ReportController starter en forekomst av
        //IReportRepository på riktig måte. Det sikrer at kontrolleren kan opprettes med det falske depotet
        //uten å støte på nullreferanseproblemer.

        [Test]
        public void Constructor_InjectsIReportRepository()
        {
            // Lager en mock for IReportRepository-grensesnittet
            var reportRepositoryMock = new Mock<IReportRepository>();

            // Lager en forekomst av ReportController, injiser det falske depotet
            var reportController = new ReportController(reportRepositoryMock.Object);

            // Bekrefter at ReportController-forekomsten ikke er null
            Assert.IsNotNull(reportController);
          
        }

        //Denne testen verifiserer at indekshandlingen til ReportController returnerer riktig visning med forventet model,
        //og sikrer at kontrolleren behandler og presenterer data fra det falske depotet på riktig måte.
        [Test]
        public void Index_ReturnsCorrectViewModel()
        {
           
            // Mock IReportRepository
            var reportRepositoryMock = new Mock<IReportRepository>();

            // Setter opp mock for å returnere spesifikke data for en gitt ServiceOrderId
            var serviceOrderId = 1; 
            var expectedReports = new List<Report>
            {
                new Report { ReportId = 1, ServiceOrderId = serviceOrderId, Mechanic = "John", ServiceType = "Repair" },
                new Report { ReportId = 2, ServiceOrderId = serviceOrderId, Mechanic = "Jane", ServiceType = "Maintenance" }
            };

            reportRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedReports);

            // Lager en forekomst av ReportController, sette i gang det falske depotet
            var reportController = new ReportController(reportRepositoryMock.Object);

            // Kaller på indekshandlingen med den angitte serviceOrderId
            var result = reportController.Index(serviceOrderId) as ViewResult;

            // Bekrefter at resultatet er et visningsresultat
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);

            // Bekrefter at modellen i visningsresultatet er av typen
            var model = result.Model as List<ReportViewModel>;
            Assert.IsNotNull(model);

             // Bekrefter at den returnerte visningsmodellen samsvarer med forventningene
            Assert.AreEqual(expectedReports.Count, model.Count);

            // Sjekker individuelle egenskaper for en spesifikk rapport initiate
            Assert.AreEqual(expectedReports[0].Mechanic, model[0].Mechanic);
            Assert.AreEqual(expectedReports[0].ServiceType, model[0].ServiceType);

            
        }

        //Denne testen hjelper til med å validere integrasjonen mellom kontrolleren og depotet,
        //og sikrer at data sendes riktig og de nødvendige depotmetodene påkalles.
        [Test]
        public void Post_CorrectlyUpsertsReportAndRedirectsToIndex()
        {
         
            // Mock IReportRepository
            var reportRepositoryMock = new Mock<IReportRepository>();

            // Lag en forekomst av ReportController, injiser det falske depotet
            var reportController = new ReportController(reportRepositoryMock.Object);

            // Create a sample ReportFullViewModel for testing
            var viewModel = new ReportFullViewModel
            {
                UpsertModel = new ReportViewModel
                {
                    ReportId = 1,
                    ServiceOrderId = 123, 
                    Mechanic = "John",
                    ServiceType = "Repair",
                    
                }
            };

            // Kaller Post-handlingen med eksempelvisningsmodellen
            var result = reportController.Post(viewModel) as RedirectToActionResult;

            // Bekrefter at resultatet er et RedirectToActionResult
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName); // Sjekker at handlinger er retunert til Index

            // Bekrefter at Upsert-metoden til det falske depotet kalles minst én gang
            reportRepositoryMock.Verify(
                repo => repo.Upsert(It.IsAny<Report>()),
                Times.AtLeastOnce, 
                "Upsert method should be called");

            Assert.AreEqual(viewModel.UpsertModel.ServiceOrderId, result.RouteValues["id"]);
        }

        //Denne testen sjekker om [ValidateAntiForgeryToken]-attributtet er til stede på Post-handlingen til ReportController.
        //Hvis attributtet brukes, består testen; ellers mislykkes den med en passende påstandsmelding.
        [Test]
        public void Post_Action_HasValidateAntiForgeryTokenAttribute()
        {
            // GET typen ReportController ved å bruke refleksjon
            var reportControllerType = typeof(ReportController);

            // GET Post-metoden til ReportController
            var postMethod = reportControllerType.GetMethod("Post");

            // Bruker refleksjon for å få tilpassede attributter brukt på Post-metoden
            var validateAntiForgeryTokenAttribute = postMethod.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), true)
                .FirstOrDefault() as ValidateAntiForgeryTokenAttribute;

            // Bekrefter at ValidateAntiForgeryTokenAttribute ikke er null som indikerer at den brukes på Post-handlingen
            Assert.IsNotNull(validateAntiForgeryTokenAttribute, "ValidateAntiForgeryTokenAttribute is not applied on the Post action.");
        }


    }

}
