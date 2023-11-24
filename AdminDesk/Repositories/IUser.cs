using AdminDesk.Entities;

namespace AdminDesk.Repositories
{
    public interface IUserRepository
    {
        Task<AdminDesk.Entities.User?> GetByEmailAsync(string email);
        void Upsert(User user);
        User Get(string Id);
        List<User> GetAll();
        
    }
}

