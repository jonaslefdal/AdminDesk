using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IDIbenRepository
    {
        void Upsert(Iben iben);
        Iben Get(int id);
        List<Iben> GetAll();
    }
}
