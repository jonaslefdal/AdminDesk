using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nøsted_Serviceordre_Behandling.Models;

namespace Nøsted_Serviceordre_Behandling.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Nøsted_Serviceordre_Behandling.Models.Serviceordre> Serviceordre { get; set; } = default!;
    }
}