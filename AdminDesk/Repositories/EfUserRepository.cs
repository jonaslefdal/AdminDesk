using AdminDesk.DataAccess;
using AdminDesk.Entities;
using AdminDesk.Repositories;
using Microsoft.EntityFrameworkCore;
using static AdminDesk.Areas.Identity.Pages.Account.Manage.IndexModel;

public class EfUserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public EfUserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public User Get(string UserId)
    {
        return _dataContext.User.FirstOrDefault(x => x.UserId == UserId);
    }

    public List<User> GetAll()
    {
        return _dataContext.User.ToList();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _dataContext.Users
            .Where(u => u.Email == email)
            .Join(_dataContext.UserDisabled,
                u => u.Id,
                ud => ud.UserId,
                (u, ud) => new { User = u, UserDisabled = ud })
            .Select(joined => new User
            {
                UserId = joined.User.Id,
                // Map other properties as needed
                Disabled = joined.UserDisabled != null ? joined.UserDisabled.Disabled : false
            })
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<List<UserWithDisabledInfo>> GetAllWithDisabledInfoAsync()
    {
        return await _dataContext.Users
            .Join(
                _dataContext.UserDisabled,
                u => u.Id,
                ud => ud.UserId,
                (u, ud) => new UserWithDisabledInfo
                {
                    User = u,
                    UserDisabled = ud
                })
            .ToListAsync();
    }

    public void Upsert(User user)
    {
        var existing = Get(user.UserId);
        if (existing != null)
        {
            existing.Disabled = !existing.Disabled;
            _dataContext.SaveChanges();
            return;
        }

        user.Disabled = true;
        _dataContext.Add(user);
        _dataContext.SaveChanges();
    }
}
