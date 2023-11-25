using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminDesk.Entities; // Assuming UserDisabled entity is defined here
using AdminDesk.Repositories; // Import your IUserRepository
using static AdminDesk.Areas.Identity.Pages.Account.Manage.IndexModel;

namespace AdminDesk.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository; // Inject your repository


        public IndexModel(UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;

        }

        public List<IdentityUser> Users { get; set; }

        public UserManager<IdentityUser> UserManager => _userManager;

        public List<UserWithDisabledInfo> UsersWithDisabledInfo { get; set; }



        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();

            Users = await _userManager.Users.ToListAsync();

            UsersWithDisabledInfo = await _userRepository.GetAllWithDisabledInfoAsync();
        }

        public class UserWithDisabledInfo
        {
            public IdentityUser User { get; set; }
            public User UserDisabled { get; set; }
        }
    }
}
