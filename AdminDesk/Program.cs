using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using AdminDesk.DataAccess;
using AdminDesk.Repositories;

public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(x => x.AddServerHeader = false);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        SetupDataConnections(builder);



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();

        app.UseRouting();



        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllers();


        app.Run();
    }

    private static void SetupDataConnections(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ISqlConnector, SqlConnector>();

        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("MariaDb"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MariaDb")));
        });
        //builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        builder.Services.AddScoped<IServiceOrdreRepository, EfServiceOrdreRepository>();
        builder.Services.AddScoped<IDyrRepository, EfDyrRepository>();
        //builder.Services.AddSingleton<IUserRepository, SqlUserRepository>();
        //builder.Services.AddSingleton<IUserRepository, DapperUserRepository>();
    }
}