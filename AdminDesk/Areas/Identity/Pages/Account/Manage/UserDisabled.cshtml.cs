
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AdminDesk.Models.User;
using AdminDesk.Repositories;
using AdminDesk.Entities;
using AdminDesk.Models.User;
using AdminDesk.Models.ServiceOrder;
using Microsoft.AspNetCore.Authorization;

namespace AdminDesk.Areas.Identity.Pages.Account.Manage
    {
    [Authorize(Policy = "RequireAdminRole")]
    public class UserDisabledModel : PageModel
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly IUserRepository _userRepository;

            public UserDisabledModel(UserManager<IdentityUser> userManager, IUserRepository userRepository)
            {
                _userManager = userManager;
                _userRepository = userRepository;
            }

        [BindProperty]
        public IdentityUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(UserFullViewModel userdisabled, string id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            User = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);


            var thisspesificuser = await _userManager.FindByIdAsync(User.Id);

            if (thisspesificuser == null)
            {
                return RedirectToPage("/Index");
            }



            var UserDisabled = new User
            {
                UserId = thisspesificuser.Id,
                Disabled = userdisabled.UpsertModel.Disabled,
            };

            _userRepository.Upsert(UserDisabled);

                return RedirectToPage("./Users");
                // Redirect back to the user management page
            }
        }
    }

