using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdminDesk.Models;

namespace AdminDesk.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
    options)
    : base(options)
    {
    }
    public DbSet<AdminDesk.Models.Serviceordre>
        Serviceordre { get; set; } = default!;
        }
        }
