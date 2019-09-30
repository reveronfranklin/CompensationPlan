using Compensation.Client.Auth;
using Compensation.Client.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Compensation.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CuotaVentasGerenteService>();
            services.AddSingleton<TipoPagoService>();
            services.AddSingleton<SysFileService>();
            services.AddSingleton<FlatComisionService>();
            services.AddSingleton<CalculoService>();
            services.AddSingleton<OficinaService>();
            services.AddSingleton<VendedorService>();
            services.AddSingleton<TemporalService>();
            services.AddSingleton<PeriodoService>();
            services.AddSingleton<HistoricoService>();
            services.AddSingleton<TasaAñoMesService>();
            services.AddSingleton<RangoCumplimientoCuotaGeneralService>();
            services.AddSingleton<PorcCantidadCategoriasCubiertasService>();
            services.AddSingleton<ResumenOficinaTemporalService>();
            services.AddSingleton<ProductoCuotaService>();
            services.AddSingleton<CuotaVentasService>();
            services.AddSingleton<OrdenesPignoradasService>();
            services.AddSingleton<AñoMesordenService>();
            services.AddSingleton<FlatComisionGerenteService>();
            services.AddSingleton<PagoManualService>();

            services.AddAuthorizationCore();
            //services.AddScoped<AuthenticationStateProvider, AuthStateProviderFalso>();

            services.AddScoped<JWTAuthenticationProvider>();
            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());
            services.AddScoped<ILoginService, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());


            services.AddBlazorContextMenu();
           


        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
