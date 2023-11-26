using AdminDesk.Entities;
using static AdminDesk.Areas.Identity.Pages.Account.Manage.IndexModel;

namespace AdminDesk.Repositories
{
    public interface IUserRepository
    {
        Task<AdminDesk.Entities.User?> GetByEmailAsync(string email);
        void Upsert(User user);
        User Get(string Id);
        List<User> GetAll();
        Task<List<UserWithDisabledInfo>> GetAllWithDisabledInfoAsync();


    }
}

