using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface ICustomerRepository
    {
        void Upsert(Customer customer);
        Customer Get(int CustomerId);
        List<Customer> GetAll();
        
    }
}
