using AdminDesk.DataAccess;
using AdminDesk.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminDesk.Repositories
{
    public class EfServiceOrderRepository : IServiceOrderRepository
    {
        private readonly DataContext _dataContext;

        public EfServiceOrderRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public ServiceOrder Get(int ServiceOrderId)
        {
            return _dataContext.ServiceOrder
            .Include(so => so.Customer) // Include related Customer data
            .FirstOrDefault(x => x.ServiceOrderId == ServiceOrderId);

        }

        public List<ServiceOrder> GetAll()
        {
            return _dataContext.ServiceOrder
            .Include(so => so.Customer) // Include related Customer data
            .ToList();
        
    }

        public void Upsert(ServiceOrder serviceorder)
        {
            var existing = _dataContext.ServiceOrder
            .Include(so => so.Customer) // Include related Customer data
            .FirstOrDefault(x => x.ServiceOrderId == serviceorder.ServiceOrderId);

            if (existing != null)
            {
                // Update existing ServiceOrder entity with new values
                _dataContext.Entry(existing).CurrentValues.SetValues(serviceorder);

                // Update the associated Customer entity
                if (serviceorder.Customer != null)
                {
                    _dataContext.Entry(existing.Customer).CurrentValues.SetValues(serviceorder.Customer);
                }
            }
            else
            {
                // Handle the case where the ServiceOrder does not exist
                serviceorder.ServiceOrderId = 0;
                _dataContext.Add(serviceorder);
            }

            _dataContext.SaveChanges();
        }
    }
}