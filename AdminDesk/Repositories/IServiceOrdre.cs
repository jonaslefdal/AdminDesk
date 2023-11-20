using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IServiceOrderRepository
    {
        void Upsert(ServiceOrder serviceorder);
        ServiceOrder Get(int ServiceOrderId);
        List<ServiceOrder> GetAll();
        
    }
}
