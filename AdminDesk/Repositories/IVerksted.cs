using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IVerkstedRepository
    {
        void Upsert(Verksted verksted);
        Verksted Get(int VerkstedId);
        List<Verksted> GetAll();
    }
}
