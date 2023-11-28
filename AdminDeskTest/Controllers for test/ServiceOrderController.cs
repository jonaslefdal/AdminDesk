using System;
using System.Collections.Generic;
using System.Linq;
using AdminDesk.Controllers;
using AdminDesk.Entities;
using AdminDesk.Models.ServiceOrder;
using AdminDesk.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class ServiceOrderControllerTests
{
    //  Testen bekrefter at mapping fra ServiceOrder til ServiceOrderViewModel er gyldig.
    //  Den sjekker at de tilordnede egenskapene, inkludert nestede egenskaper som Customer, er riktig fylt ut.
    // (Mapping refererer til prosessen med å konvertere data mellom to forskjellige strukturer,
    // data som blir brukt av controller og data lagret og hentet fra model).


    // Testen sikrer at AutoMapper er riktig konfigurert og fungerer for de spesifiserte
    // tilordningene i ServiceOrderMappingProfile.offentlig klasse ServiceOrderControllerTests
       [Fact]
    public void Map_ServiceOrderToServiceOrderViewModel_ShouldBeValid()
    {
        // Mock avhengigheter
        var serviceOrderRepositoryMock = new Mock<IServiceOrderRepository>();
        var reportRepositoryMock = new Mock<IReportRepository>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();

        // Bruker en konkret implementering av UserManager<IdentityUser> for testing
        var userManager = new UserManager<IdentityUser>(
            new Mock<IUserStore<IdentityUser>>().Object,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        // Setter opp AutoMapper
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceOrderMappingProfile>();
        });
        IMapper mapper = configuration.CreateMapper();

        // Lag kontrolleren med mock avhengigheter og UserManager
        var controller = new ServiceOrderController(
            serviceOrderRepositoryMock.Object,
            reportRepositoryMock.Object,
            customerRepositoryMock.Object,
            userManager
        );

        // Eksempeldata
        var serviceOrderEntity = new ServiceOrder
        {
            // Fyll ut med eksempeldata
        };

        // Utfør kartlegging med AutoMapper
        var serviceOrderViewModel = mapper.Map<ServiceOrderViewModel>(serviceOrderEntity);

        // Grunnleggende påstander
        Xunit.Assert.NotNull(serviceOrderViewModel);
        Xunit.Assert.Equal(serviceOrderEntity.ServiceOrderId, serviceOrderViewModel.ServiceOrderId);

        // Nestede kartleggingspåstander
        if (serviceOrderEntity.Customer != null)
        {
            Xunit.Assert.NotNull(serviceOrderViewModel.Customer);
            Xunit.Assert.Equal(serviceOrderEntity.Customer.CustomerId, serviceOrderViewModel.Customer.CustomerId);
        }

    }

    // AutoMapper-profil for ServiceOrder og Customer mappings
    public class ServiceOrderMappingProfile : Profile
    {
    public ServiceOrderMappingProfile()
    {
        CreateMap<ServiceOrder, ServiceOrderViewModel>();
        CreateMap<Customer, CustomerViewModel>();
    }
}

    //------------------------------------------------------------------------------------------
    [Fact]
    public void NyServiceOrdre_Action_Returns_View_For_Authorized_User()
    {
        // Arrange
        var userManagerMock = GetUserManagerMock(hasAdminRole: true);
        var controller = SetupControllerWithAuthorizedUser(userManagerMock.Object);

        // Act
        var result = controller.NyServiceOrdre();

        // Assert
        Xunit.Assert.NotNull(result);

        var viewResult = (ViewResult)result;
        Xunit.Assert.Equal("NyServiceOrdre", viewResult.ViewName);
    }

    private ServiceOrderController SetupControllerWithAuthorizedUser(UserManager<IdentityUser> userManager)
    {
        var controller = new ServiceOrderController(null, null, null, userManager);

        var context = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = GetClaimsPrincipalWithRole("RequireAdminRole")
            }
        };

        controller.ControllerContext = context;

        return controller;
    }

    private ClaimsPrincipal GetClaimsPrincipalWithRole(string role)
    {
        return new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
                new Claim(ClaimTypes.Role, role)
        }));
    }

    private Mock<UserManager<IdentityUser>> GetUserManagerMock(bool hasAdminRole = false)
    {
        var users = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "testUserId",
                    UserName = "testUser",
                }
            };

        var userStoreMock = new Mock<IUserStore<IdentityUser>>();
        userStoreMock.Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(users.FirstOrDefault());

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            userStoreMock.Object,
            null, null, null, null, null, null, null, null);


        return userManagerMock;
    }
}


