using AdminDesk.DataAccess;//Importerer for å håndtere databasetjenester i AdminDesk
using AdminDesk.Entities;//Importerer entitetsklasser fra AdminDesk
using Microsoft.EntityFrameworkCore;//Imporerer for å bruke Entity Framework, en ORM


namespace AdminDesk.Repositories//Ærklerer navneområde for repositories i AdminDesk
{
    public class EfCustomerRepository : ICustomerRepository// EfCustomerRepository implementerer ICustomerRepository interface med bruk av entity framework
    {
        private readonly DataContext _dataContext;//Privat felt for å holde DataContext

        //Constructor til å initalisere repository
        public EfCustomerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Customer Get(int CustomerId) //Henter kunde basert på ID
        {

            return _dataContext.Customer.FirstOrDefault(x => x.CustomerId == CustomerId);//Returnerer første kunde som passer med ID



        }


        //Henter kundeliste i database
        public List<Customer> GetAll()
        {
            return _dataContext.Customer.ToList();//Konverter og returnerer til en liste

        }


        public void Upsert(Customer customer) //Upserter en kunde i databasen
        {
            //Sjekker om kunde eksisterer
            var existing = Get(customer.CustomerId);
            if (existing != null)
            {
                // Update existing customer entity with new values
                _dataContext.Entry(existing).CurrentValues.SetValues(customer);
            }
            else
            {
                customer.CustomerId = 0;
                _dataContext.Add(customer);
            }

            _dataContext.SaveChanges();
        }

    }
}