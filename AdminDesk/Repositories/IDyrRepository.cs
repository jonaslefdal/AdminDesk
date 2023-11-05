using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IDyrRepository
    {
        void Upsert(Dyr dyr);
        Dyr Get(int id);
        List<Dyr> GetAll();
    }
}
