using AdminDesk.DataAccess;
using AdminDesk.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AdminDesk.Repositories
{
    public class EfCheckListRepository : ICheckListRepository
    {
        private readonly DataContext _dataContext;

        public EfCheckListRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CheckList Get(int CheckListId)
        {
            return _dataContext.CheckList.FirstOrDefault(x => x.CheckListId == CheckListId);
        }

        public List<CheckList> GetAll()
        {
            return _dataContext.CheckList.ToList();
        }

        public CheckList GetByServiceOrderId(int serviceOrderId)
        {
            return _dataContext.CheckList.FirstOrDefault(x => x.ServiceOrderId == serviceOrderId);
        }

        public void Upsert(CheckList checklist)
        {
            // Assuming CheckList has an Id property
            if (checklist.CheckListId == 0)
            {
                _dataContext.CheckList.Add(checklist); // New item, add to the database
            }
            else
            {
                _dataContext.CheckList.Update(checklist); // Existing item, update in the database
            }

            _dataContext.SaveChanges();
        }
    }
}
