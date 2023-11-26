using AdminDesk.Areas.Identity.Pages.Account.Manage;//Importer klasser for håndtering av brukerprofiler
using AdminDesk.Entities;//Importerer entitetsklasser fra AdminDesk
using Microsoft.AspNetCore.Identity;//Importerer klasser for autentisering og autorisasjon
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;//Imporerterer klasser for integrasjon av brukeridentitet med EF
using Microsoft.EntityFrameworkCore;//Imporerer for å bruke Entity Framework, en ORM

namespace AdminDesk.DataAccess//Navneområde for DataAccess klasser
{
    public class DataContext : IdentityDbContext<IdentityUser>//DataContext klasse som arver fra IdentityDbContext 
    {



        public DataContext(DbContextOptions<DataContext> options) : base(options)//Setter opp DataContext ved hjelp av DbContextOptions
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)//Kobler ServiceOrdre-entity til serviceordre-tabell
        {
            modelBuilder.Entity<ServiceOrder>()//Kobler serviceorder-entitet til serviceordre-tabell via primærnøkkel
                .ToTable("serviceorders")
                .HasKey(x => x.ServiceOrderId);

            modelBuilder.Entity<ServiceOrder>()// Oppretter en relasjon mellom ServiceOrder-og Customer-entitet.
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId);


            // Koden nedenfor konfigurerer diverse entiterer ved å koble dem til deres  tabeller. Det blir definert primærnøkler i databasen
            modelBuilder.Entity<Verksted>().ToTable("Verksted").HasKey(x => x.VerkstedId);

            modelBuilder.Entity<Report>().ToTable("Report").HasKey(x => x.ReportId);

            modelBuilder.Entity<Customer>().ToTable("Customer").HasKey(x => x.CustomerId);



            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Verksted> Verksted { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }
        public DbSet<Customer> Customer { get; set; }

    }
}