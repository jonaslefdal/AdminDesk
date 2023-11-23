using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AdminDesk.Models;  // Import your custom ApplicationUser class

namespace AdminDesk.Areas.Identity.Pages.Account.Manage
{
    public class UserDisabledModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDisabledModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the user and cast it to ApplicationUser
            User = await _userManager.FindByIdAsync(id) as ApplicationUser;

            if (User == null)
            {
                return NotFound();
            }

            // Set Disabled to true (1)
            User.Disabled = true;

            // Save changes to the database
            await _userManager.UpdateAsync(User);

            // Optionally, you can perform additional actions here

            return RedirectToPage("./Users");
            // Redirect back to the user management page
        }
    }
}
