using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public enum LockoutStatusEnum
{
    På, // "On" in Norwegian
    Av  // "Off" in Norwegian
}

namespace AdminDesk.Areas.Identity.Pages.Account.Manage
{
    public class UserEditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserEditModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public IdentityUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _userManager.FindByIdAsync(User.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = User.UserName;
            existingUser.Email = User.Email;
            existingUser.PhoneNumber = User.PhoneNumber;
            existingUser.LockoutEnabled = User.LockoutEnabled;

            // Update other fields as needed

            var result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded)
            {
                // Handle successful update
                return RedirectToPage("./Users");
            }
            else
            {
                // Handle update failure
                return RedirectToPage("Index");
            }
        }
    }
}
