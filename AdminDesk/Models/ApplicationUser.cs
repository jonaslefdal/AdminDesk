using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public bool Disabled { get; set; }
}
