using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IServiceOrdreRepository
    {
        void Upsert(ServiceOrdre serviceordre);
        ServiceOrdre Get(int ServiceOrderId);
        List<ServiceOrdre> GetAll();
    }
}
