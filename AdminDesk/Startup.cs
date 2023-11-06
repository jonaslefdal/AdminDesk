using AdminDesk.DataAccess;
using AdminDesk.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace AdminDesk
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app)
        {

            // Security set up of HTTP headers

            app.Use(async (context, next) =>
            {

                context.Response.Headers.Add("X-Xss-Protection", "1");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    "default-src 'self'; " +
                    "img-src 'self'; " +
                    "font-src 'self'; " +
                    "style-src 'self'; " +
                    "script-src 'self'" +
                    "frame-src 'self';" +
                    "connect-src 'self';");
                await next();
            });

            // To enforce HTTPS connection
            app.UseHsts();



            //// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
        private static void SetupDataConnections(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ISqlConnector, SqlConnector>();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("MariaDb"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MariaDb")));
            });
            //builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            builder.Services.AddScoped<IDyrRepository, EfDyrRepository>();
            //builder.Services.AddSingleton<IUserRepository, SqlUserRepository>();
            //builder.Services.AddSingleton<IUserRepository, DapperUserRepository>();
        }
    }

}
