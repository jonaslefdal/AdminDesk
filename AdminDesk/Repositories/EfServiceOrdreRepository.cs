using AdminDesk.DataAccess;
using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public class EfServiceOrdreRepository : IServiceOrdreRepository
    {
        private readonly DataContext _dataContext;

        public EfServiceOrdreRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public ServiceOrdre Get(int ServiceOrderId)
        {
            return _dataContext.ServiceOrdre.FirstOrDefault(x => x.ServiceOrderId == ServiceOrderId);
        }

        public List<ServiceOrdre> GetAll()
        {
            return _dataContext.ServiceOrdre.ToList();
        }

        public void Upsert(ServiceOrdre serviceordre)
        {
            var existing = Get(serviceordre.ServiceOrderId);
            if(existing != null)
            {
                existing.CustomerName = serviceordre.CustomerName;
                _dataContext.SaveChanges();
                return;
            }
            serviceordre.ServiceOrderId = 0;
            _dataContext.Add(serviceordre);
            _dataContext.SaveChanges();
        }
    }
}
