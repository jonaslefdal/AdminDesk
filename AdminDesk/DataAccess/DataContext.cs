using AdminDesk.Areas.Identity.Pages.Account.Manage;
using AdminDesk.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminDesk.DataAccess
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
     


    public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceOrder>()
                .ToTable("serviceorders")
                .HasKey(x => x.ServiceOrderId);

            modelBuilder.Entity<ServiceOrder>()
                .HasOne(s => s.Customer)  // Assuming Customer is a navigation property in ServiceOrder
                .WithMany()
                .HasForeignKey(s => s.CustomerId);

            

            modelBuilder.Entity<Verksted>().ToTable("Verksted").HasKey(x => x.VerkstedId);

            modelBuilder.Entity<Report>().ToTable("Report").HasKey(x => x.ReportId);

            modelBuilder.Entity<Customer>().ToTable("Customer").HasKey(x => x.CustomerId);

            modelBuilder.Entity<User>().ToTable("UserDisabled").HasKey(x => x.UserId);




            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Verksted> Verksted { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<User> UserDisabled { get; set; }





    }
}
