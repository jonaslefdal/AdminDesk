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
    .       ToList();
        
    }

        public void Upsert(ServiceOrder serviceorder)
        {
            var existing = Get(serviceorder.ServiceOrderId);
            if(existing != null)
            {
                existing.ServiceOrderId = serviceorder.ServiceOrderId;
                _dataContext.SaveChanges();
                return;
            }
            serviceorder.ServiceOrderId = 0;
            _dataContext.Add(serviceorder);
            _dataContext.SaveChanges();
        }
    }
}
