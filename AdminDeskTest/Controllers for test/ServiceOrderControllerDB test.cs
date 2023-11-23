using AdminDesk.DataAccess;
using AdminDesk.Entities;
using AdminDesk.Repositories;
using NUnit.Framework.Internal;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace AdminDeskTest.Repositories
{
    public class EfServiceOrderRepositoryTests
    {
        //Disse testene validerer den grunnleggende funksjonaliteten til Upsert- og Get-metodene i EfServiceOrderRepository.
        //Upsert-testen sjekker om en ServiceOrder kan lagres i minnedatabasen.
        //Get-testen sjekker om Get-metoden henter en riktig ServiceOrder fra minnedatabasen.

        [Fact]
        public void Upsert_ServiceOrder_ShouldStoreInDatabase()
        {
            
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase") 
                .Options;

            using (var context = new DataContext(options))
            {
                var serviceOrderRepository = new EfServiceOrderRepository(context);

                // Oppretter et ServiceOrder-objekt som skal lagres
                var serviceOrder = new ServiceOrder
                {
                    ServiceOrderId = 1,
                    Mechanic = "John",
                };

               
                serviceOrderRepository.Upsert(serviceOrder);

                // Bekrefter at ServiceOrderen ble lagret i minnet
                var storedServiceOrder = context.ServiceOrder.FirstOrDefault(x => x.ServiceOrderId == serviceOrder.ServiceOrderId);
                Xunit.Assert.NotNull(storedServiceOrder);
                Xunit.Assert.Equal(serviceOrder.Mechanic, storedServiceOrder.Mechanic);
            }
        }

        [Fact]
        public void Get_ServiceOrderById_ShouldRetrieveFromDatabase()
        {
            
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase") 
                .Options;

            using (var context = new DataContext(options))
            {
                var serviceOrderRepository = new EfServiceOrderRepository(context);

                // Oppretter og legger til en ServiceOrder til minnedatabasen
                var expectedServiceOrder = new ServiceOrder
                {
                    ServiceOrderId = 1,
                    Mechanic = "John",
                };

                context.ServiceOrder.Add(expectedServiceOrder);
                context.SaveChanges();

                // Henter ServiceOrder ved å bruke repository-metoden
                var result = serviceOrderRepository.Get(1);

                // Bekrefter at den hentede ServiceOrderen samsvarer med den forventede
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal(expectedServiceOrder.Mechanic, result.Mechanic);
               
            }
        }

    }
}
