using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using AdminDesk.Controllers;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Models.ServiceOrder;

namespace AdminDeskTest
{
    [TestFixture]
    public class ServiceOrderControllerTests
    {
        //Testen sikrer at å kalle opp NyServiceOrdre-handlingen i ServiceOrderController returnerer
        //forventet visning og initialiserer modellen riktig.
        [Test]
        public void NyServiceOrdre_ReturnsCorrectViewAndInitializesModel()
        {
            // Mock-forekomster for repositoriene
            var serviceOrderRepositoryMock = new Mock<IServiceOrderRepository>();
            var reportRepositoryMock = new Mock<IReportRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            // Opprett en forekomst av ServiceOrderController med mock repositories
            var serviceOrderController = new ServiceOrderController(
                serviceOrderRepositoryMock.Object,
                reportRepositoryMock.Object,
                customerRepositoryMock.Object
            );

            // Kaller NyServiceOrdre-aksjonen og cast resultatet til
            var result = serviceOrderController.NyServiceOrdre() as ViewResult;

            // Bekreft at resultatet ikke er null og har riktig visningsnavn
            Assert.IsNotNull(result);
            Assert.AreEqual("NyServiceOrdre", result.ViewName);

            // Trekker ut modellen fra resultatet og bekreft typen
            var model = result.Model as ServiceOrderFullViewModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.UpsertModel);

            // Sjekk at CreatedDate-egenskapen er satt til gjeldende dato
            Assert.AreEqual(DateTime.Now.Date, model.UpsertModel.CreatedDate.Date);
        }

        // Testen sikrer at den spesifikke handlingen til ServiceOrderController returnerer riktig visning og modell.
        // Den bekrefter at serviceordredetaljene og tilknyttede rapporter er korrekt tilordnet CompositeViewModel.
        [Test]
        public void Spesific_ReturnsCorrectViewAndModel()
        {
            // Mock-forekomster for repositoriene
            var serviceOrderRepositoryMock = new Mock<IServiceOrderRepository>();
            var reportRepositoryMock = new Mock<IReportRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            // Oppretter en forekomst av ServiceOrderController med mock repositories
            var serviceOrderController = new ServiceOrderController(
                serviceOrderRepositoryMock.Object,
                reportRepositoryMock.Object,
                customerRepositoryMock.Object
            );

            // Setter opp falske data for en spesifikk tjenesteordre og tilhørende rapporter
            var serviceOrderId = 1;
            var expectedServiceOrder = new ServiceOrder
            {
                ServiceOrderId = serviceOrderId,
                Mechanic = "John",

                Customer = new Customer
                {
                    
                }
            };

            var expectedReports = new List<Report>
            {
                new Report { ReportId = 1, ServiceOrderId = serviceOrderId, Mechanic = "Mechanic1", ServiceType = "Type1" },
                new Report { ReportId = 2, ServiceOrderId = serviceOrderId, Mechanic = "Mechanic2", ServiceType = "Type2" }
            };

            serviceOrderRepositoryMock.Setup(repo => repo.Get(serviceOrderId)).Returns(expectedServiceOrder);
            reportRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedReports);

            // Kaller på den spesifikke handlingen med den spesifikke serviceordre-IDen
            var result = serviceOrderController.Spesific(serviceOrderId) as ViewResult;

            // Bekrefter at resultatet ikke er null og har riktig visningsnavn
            Assert.IsNotNull(result);
            Assert.AreEqual("Spesific", result.ViewName);

            // Treker ut modellen fra resultatet og bekreft typen
            var model = result.Model as CompositeViewModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.ServiceOrderModel);
            Assert.IsNotNull(model.ReportModel);

            // Sjekker at serviceordredetaljene er riktig kartlagt
            Assert.AreEqual(expectedServiceOrder.Mechanic, model.ServiceOrderModel.UpsertModel.Mechanic);

            // Sjekker at de tilknyttede rapportene er riktig mapped
            Assert.AreEqual(expectedReports.Count, model.ReportModel.ReportList.Count);

            for (int i = 0; i < expectedReports.Count; i++)
            {
                Assert.AreEqual(expectedReports[i].Mechanic, model.ReportModel.ReportList[i].Mechanic);
                Assert.AreEqual(expectedReports[i].ServiceType, model.ReportModel.ReportList[i].ServiceType);
                
            }

        }

        //Denne testen setter opp en ugyldig modelltilstand ved å legge til en feil i et spesifikt felt
        //og verifiserer deretter at kontrolleren returnerer "NyServiceOrdre"-visningen med den ugyldige modellen når
        //Post-handlingen kalles med den ugyldige modellen.
        [Test]
        public void Post_InvalidModelState_ReturnsViewWithModel()
        {
            // Mock-forekomster for repositoriene
            var serviceOrderRepositoryMock = new Mock<IServiceOrderRepository>();
            var reportRepositoryMock = new Mock<IReportRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            // Oppretter en forekomst av ServiceOrderController med mock repositories
            var serviceOrderController = new ServiceOrderController(
                serviceOrderRepositoryMock.Object,
                reportRepositoryMock.Object,
                customerRepositoryMock.Object
            );

            // Setter opp en ugyldig modelltilstand
            serviceOrderController.ModelState.AddModelError("SomeField", "The field is required");

            // Oppretter et eksempel på ugyldig ServiceOrderFullViewModel for testing
            var invalidViewModel = new ServiceOrderFullViewModel
            {
                UpsertModel = new ServiceOrderViewModel
                {
                    
                }
            };
    
            var result = serviceOrderController.Post(invalidViewModel) as ViewResult;


            // Bekrefter at resultatet ikke er null og har riktig visningsnavn
            Assert.IsNotNull(result);
            Assert.AreEqual("NyServiceOrdre", result.ViewName);

            // Trekker ut modellen fra resultatet og bekreft typen
            var model = result.Model as ServiceOrderFullViewModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.UpsertModel);

            Assert.IsTrue(serviceOrderController.ModelState.ContainsKey("SomeField"));
            Assert.AreEqual("The field is required", serviceOrderController.ModelState["SomeField"].Errors.First().ErrorMessage);
        }

    }

}
