using Compensaction.Share;
using Compensation.Api.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.Api
{
    public class Calcular
    {
        private readonly CompensationDbContext _context;
        public Calcular(CompensationDbContext context)
        {
            _context = context;
        }


        public async Task EjecutarCalculo()
        {
            var proceso = _context.PCProceso.Where(p => p.Iniciado == false).FirstOrDefault();
            if (proceso != null)
            {
                proceso.Iniciado = true;
                await _context.SaveChangesAsync();

                Calculo calculo = new Calculo(_context);
                List<PCComisionesTemporal> pCTemporal = new List<PCComisionesTemporal>();
                pCTemporal = _context.PCComisionesTemporal.ToList();
                if (pCTemporal != null)
                {
                    foreach (var item in pCTemporal)
                    {
                        calculo.CalcularRecibo(item.Id);
                        proceso.RegistrosProcesados++;
                        await _context.SaveChangesAsync();
                    }
                }

                Console.WriteLine("Task started");
                proceso.Culminado = true;
                await _context.SaveChangesAsync();

            }
        }
    }
}
