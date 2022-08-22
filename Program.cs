using PnPCore;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PnP.Core.Auth.Services.Builder.Configuration;
using PnP.Core.Services;

const string CertificateThumbprint = "";
const string ClientId = "";
const string TenantId = "";


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        {
            services.AddPnPCore(options =>
        {
            options.PnPContext.GraphFirst = false;
        });
            services.AddPnPCoreAuthentication(options =>
            {
                // Load the certificate to use
                X509Certificate2 cert = Utilities.LoadCertificate("", CertificateThumbprint);

                // Configure certificate based auth
                options.Credentials.Configurations.Add("CertAuth", new PnPCoreAuthenticationCredentialConfigurationOptions
                {
                    ClientId = ClientId,
                    TenantId = TenantId,
                    X509Certificate = new PnPCoreAuthenticationX509CertificateOptions
                    {
                        Certificate = cert,
                    }
                });
                options.Credentials.DefaultConfiguration = "CertAuth";
            });
        })
    .Build();

ExemplifyScoping(host.Services);

await host.RunAsync();

static void ExemplifyScoping(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    var factory = provider.GetRequiredService<IPnPContextFactory>();
    var fence = provider.GetRequiredService<Fence>();
    var sp = new SharePoint(factory, fence);
    sp.GetWebTitle().GetAwaiter().GetResult();
}