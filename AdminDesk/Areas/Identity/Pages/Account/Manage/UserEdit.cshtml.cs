using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AdminDesk.Entities;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "RequireAdminRole")]
    public class UserEditModel : PageModel
    {
        public class UserEditInputModel
        {
            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }

            // Add other properties as needed
        }

        private readonly UserManager<IdentityUser> _userManager;

        public UserEditModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public UserEditInputModel Input { get; set; }

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


            // Get the user's current roles
            var userRoles = await _userManager.GetRolesAsync(User);

            if (userRoles != null && userRoles.Any())
            {
                Input = new UserEditInputModel
                {
                    Role = userRoles.FirstOrDefault()
                };
            }
            else
            {
                // Handle the case where the user has no roles
                Input = new UserEditInputModel
                {
                    Role = "User" // Provide a default role if needed
                };
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


            var userRoles = await _userManager.GetRolesAsync(existingUser);

            // Remove the user from all current roles
            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(existingUser, role);
            }

            // Add the user to the new role
            await _userManager.AddToRoleAsync(existingUser, Input.Role);

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
