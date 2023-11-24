using AdminDesk.DataAccess;
using AdminDesk.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminDesk.Repositories
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;

        public EfCustomerRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public Customer Get(int CustomerId)
        {
            
            return _dataContext.Customer.FirstOrDefault(x => x.CustomerId == CustomerId);


        }

        public List<Customer> GetAll()
        {
            return _dataContext.Customer.ToList();
            
    }

        public void Upsert(Customer customer)
        {
            var existing = Get(customer.CustomerId);
            if(existing != null)
            {
                existing.CustomerId = customer.CustomerId;
                _dataContext.SaveChanges();
                return;
            }
            customer.CustomerId = 0;
            _dataContext.Add(customer);
            _dataContext.SaveChanges();
        }
    }
}
