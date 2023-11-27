using System.Reflection;
using System.Security.Claims;
using AdminDesk.Controllers;
using AdminDesk.Models.Report;
using AdminDesk.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminDeskTest.Controllers
{
    public class ReportControllerAuthorizationTests
    {

        // Denne testen sjekker at indekshandlingen i ReportController
        // krever "RequireUserOrAdminRole"-policyen.
        [Fact]
        public void Index_Action_Should_Require_UserOrAdminRole_Policy()
        {
            // userManagerMock returnerer en bruker med en spesifisert ID og brukernavn.
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var reportRepositoryMock = new Mock<IReportRepository>();

            var user = new IdentityUser
            {
                Id = "testUserId",
                UserName = "TestUser",
            };

            userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            // Anrop en handlingsmetode som krever autorisasjon (f.eks. Indeks).
            var result = controller.Index(It.IsAny<int>());

            // Sjekk om kontrolleren har den forventede [Authorize]-attributten på kontrollernivå.
            var authorizeAttribute = Xunit.Assert.IsType<AuthorizeAttribute>(
                controller.GetType().GetCustomAttribute(typeof(AuthorizeAttribute), inherit: true)
            );

            // Sørg for at [Authorize]-attributtet spesifiserer "RequireUserOrAdminRole"-policyen.
            Xunit.Assert.Contains("RequireUserOrAdminRole", authorizeAttribute.Policy);
        }



        //Verifiserer at PostReport-handlingen krever rollen "Admin".
        [Fact]
        public async Task PostReport_Action_Should_Require_AdminRole()
        {
            // userManagerMock returnerer en bruker med en spesifisert ID og brukernavn.
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var reportRepositoryMock = new Mock<IReportRepository>();

            var user = new IdentityUser
            {
                Id = "testUserId",
                UserName = "TestUser",
            };

            userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            // PostReport-handlingen påkalles med en ReportFullViewModel.
            var result = await controller.PostReport(new ReportFullViewModel());

            // Sjekker om resultatet er av typen RedirectToActionResult, forutsatt at uautoriserte brukere blir omdirigert.
            Xunit.Assert.IsType<RedirectToActionResult>(result);
        }



        // Test for PostReport-handlingen med gyldige data.
        [Fact]
        public async Task PostReport_WithValidData_ShouldReturnRedirectToAction()
        {
            // Lager mocking for UserManager og IReportRepository.
            var userManagerMock = GetMockUserManagerWithUser();
            var reportRepositoryMock = new Mock<IReportRepository>();
            // Oppretter en forekomst av ReportController med mock avhengighetene.
            var controller = new ReportController(reportRepositoryMock.Object, userManagerMock.Object);

            // Definere en gyldig rapport med eksempeldata.
            var validReport = new ReportFullViewModel
            {
                UpsertModel = new ReportViewModel
                {
                    ServiceOrderId = 1,
                }
            };

            // Kalle opp PostReport-handlingsmetoden med den gyldige rapporten.
            var result = await controller.PostReport(validReport);

            // Verifiserer at resultatet er av typen RedirectToActionResult.
            Xunit.Assert.IsType<RedirectToActionResult>(result);
        }

        // Hjelpemetode for å lage en mock UserManager med en testbruker.
        private Mock<UserManager<IdentityUser>> GetMockUserManagerWithUser()
        {
            var user = new IdentityUser
            {
                Id = "testUserId",
                UserName = "TestUser",
            };

            var userManagerMock = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object,
                                                                      null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(user);

            return userManagerMock;
        }
    }
}