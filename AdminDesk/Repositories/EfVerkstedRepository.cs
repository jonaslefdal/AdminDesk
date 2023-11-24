using AdminDesk.DataAccess;
using AdminDesk.Entities;
using Microsoft.EntityFrameworkCore;


namespace AdminDesk.Repositories
{ 
public class VerkstedRepository : IVerkstedRepository
{
  private readonly DataContext _dataContext;

        public VerkstedRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public Verksted Get(int VerkstedId)
        {
            return _dataContext.Verksted.FirstOrDefault(x => x.VerkstedId == VerkstedId);

        }

        public List<Verksted> GetAll()
        {
            return _dataContext.Verksted.ToList();

        }



        public void Upsert(Verksted verksted)
        {
            var existing = Get(verksted.VerkstedId);
            if (existing != null)
            {
                existing.VerkstedId = verksted.VerkstedId;
                _dataContext.SaveChanges();
                return;
            }
            verksted.VerkstedId = 0;
            _dataContext.Add(verksted);
            _dataContext.SaveChanges();
        }




    }
}