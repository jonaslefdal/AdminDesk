using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminDesk.Controllers;
using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdminDeskTest
{
    // Test klasse for ReportController
    public class ReportControllerTests
    {
        // Test for Index action
        [Fact]
        public void Index_ReturnsCorrectViewModel()
        {
            // Arrange
            var reportRepositoryMock = new Mock<IReportRepository>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>();
            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            var model = result?.Model as ReportFullViewModel;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Index", result.ViewName); // Checks if the correct view is returned
            Xunit.Assert.NotNull(model);
            Xunit.Assert.Equal(1, model.UpsertModel.ServiceOrderId); // Checks if the model has the correct ServiceOrderId
        }

        // Test for CheckList action
        [Fact]
        public void CheckList_ReturnsCorrectViewModel()
        {
            // Arrange
            var reportRepositoryMock = new Mock<IReportRepository>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>();
            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            // Act
            var result = controller.CheckList(1) as ViewResult;
            var model = result?.Model as ReportFullViewModel;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("CheckList2", result.ViewName); // Checks if the correct view is returned
            Xunit.Assert.NotNull(model);
            Xunit.Assert.Equal(1, model.UpsertModel.ServiceOrderId); // Checks if the model has the correct ServiceOrderId
        }

        // Test for PostReport-handlingen når den omdirigerer til "Spesific" ved suksess
        [Fact]
        public async Task PostReport_RedirectsToSpesificOnSuccess()
        {
            // Arrange
            var reportRepositoryMock = new Mock<IReportRepository>();
            var userManagerMock = GetUserManagerMock();
            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            var viewModel = new ReportFullViewModel
            {
                UpsertModel = new ReportViewModel
                {
                    ServiceOrderId = 123,
                    Mechanic = "John",
                    ServiceType = "Repair",
                    ReportWriteDate = DateTime.Now,
                }
            };

            // Act
            var result = await controller.PostReport(viewModel) as RedirectToActionResult;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Spesific", result.ActionName); // Sjekker om handlingen omdirigerer til "Spesific"
            reportRepositoryMock.Verify(repo => repo.Upsert(It.IsAny<Report>()), Times.Once); // Sjekker om Upsert kalles
            Xunit.Assert.Equal(viewModel.UpsertModel.ServiceOrderId, result.RouteValues["id"]); // Sjekker om riktig id er bestått
        }

        // Test for PostReport-handlingen når gjeldende bruker er null
        [Fact]
        public async Task PostReport_ReturnsBadRequestIfCurrentUserIsNull()
        {
            // Arrange
            var reportRepositoryMock = new Mock<IReportRepository>();
            var userManagerMock = GetUserManagerMock(currentUser: null);
            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            var viewModel = new ReportFullViewModel
            {
                UpsertModel = new ReportViewModel
                {
                    ServiceOrderId = 123,
                    Mechanic = "John",
                    ServiceType = "Repair",
                    ReportWriteDate = DateTime.Now,
                }
            };

            // Act
            var result = await controller.PostReport(viewModel) as BadRequestObjectResult;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Unable to determine the current user.", result.Value); // Checks if BadRequest is returned with the correct message
        }

        // Test for PostReport-handlingen når ModelState er ugyldig
        [Fact]
        public async Task PostReport_ReturnsViewWithErrorIfModelStateIsInvalid()
        {
            // Arrange
            var reportRepositoryMock = new Mock<IReportRepository>();
            var userManagerMock = GetUserManagerMock();
            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);
            controller.ModelState.AddModelError("SomeField", "Some error message");

            var viewModel = new ReportFullViewModel
            {
                UpsertModel = new ReportViewModel
                {
                    ServiceOrderId = 123,
                    Mechanic = "John",
                    ServiceType = "Repair",
                    ReportWriteDate = DateTime.Now,
                }
            };

            // Act
            var result = await controller.PostReport(viewModel) as ViewResult;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Index", result.ViewName); // Sjekker om action retunerer "Index" view
        }

        // Hjelpemetode for å lage en mock for UserManager med valgfri nåværende bruker
        private Mock<UserManager<IdentityUser>> GetUserManagerMock(IdentityUser currentUser = null)
        {
            var userManagerMock = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);

            if (currentUser != null)
            {
                userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                    .ReturnsAsync(currentUser);
            }

            return userManagerMock;
        }
    }
}
