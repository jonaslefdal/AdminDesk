using Microsoft.AspNetCore.Identity;



namespace AdminDesk.Models.ApplicationUser { 

public class ApplicationUser : IdentityUser
{
    public bool Disabled { get; set; }
}

}