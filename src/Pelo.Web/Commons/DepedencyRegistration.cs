using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pelo.Web.Services.CrmServices;

namespace Pelo.Web.Commons
{
    public static class DepedencyRegistration
    {
        public static IContainer RegisterAutofac(this IServiceCollection services,
            IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(CrmService).Assembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Populate(services);
            var container = builder.Build();

            return container;
        }
    }
}