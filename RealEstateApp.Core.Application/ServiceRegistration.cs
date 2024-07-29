using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System.Reflection;

namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient<ITipoPropiedadService, TipoPropiedadService>();
            services.AddTransient<ITipoVentaService, TipoVentaService>();
            services.AddTransient<IMejoraService, MejoraService>();
            services.AddTransient<IUserService, UserService>();

            #endregion
        }
    }
}
