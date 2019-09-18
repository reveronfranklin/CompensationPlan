using Compensaction.Share;
using Compensation.Api.Bussines;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.Api
{
    public class MyBackgroundTask : IHostedService
    {
        CompensationDbContext compensationDbContext;
        private IServiceScopeFactory serviceScopeFactory;

        public MyBackgroundTask(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                

                using (var scope=serviceScopeFactory.CreateScope())
                {
                    compensationDbContext = scope.ServiceProvider.GetService<CompensationDbContext>();

                    var proceso = compensationDbContext.PCProceso.Where(p=>p.Iniciado==false).FirstOrDefault();
                    if (proceso!=null)
                    {
                        proceso.Iniciado = true;
                        await compensationDbContext.SaveChangesAsync(cancellationToken);

                        Calculo calculo = new Calculo(compensationDbContext);
                        List<PCComisionesTemporal> pCTemporal = new List<PCComisionesTemporal>();
                        pCTemporal = compensationDbContext.PCComisionesTemporal.ToList();
                        if (pCTemporal != null)
                        {
                            foreach (var item in pCTemporal)
                            {
                                calculo.CalcularRecibo(item.Id);
                                proceso.RegistrosProcesados++;
                                await compensationDbContext.SaveChangesAsync(cancellationToken);
                            }
                        }

                        Console.WriteLine("Task started");
                        proceso.Culminado = true;
                        await compensationDbContext.SaveChangesAsync(cancellationToken);

                    }
                }
                
                await Task.Delay(50000);
            }


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
