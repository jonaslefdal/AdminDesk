using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nøsted_Serviceordre_Behandling.Data;

[assembly: HostingStartup(typeof(Nøsted_Serviceordre_Behandling.Areas.Identity.IdentityHostingStartup))]
namespace Nøsted_Serviceordre_Behandling.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
