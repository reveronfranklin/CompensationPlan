using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compensaction.Share;
using Compensation.Api.Bussines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compensation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public CalculoController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/Calculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCProceso>>> GetCalcular()
        {

           

            try
            {
                PCProceso pCProceso = new PCProceso();
                PCProceso newpCProceso = new PCProceso();
                WSMY686 periodo = new WSMY686();
                PCHistorico pCHistorico = new PCHistorico();
                periodo = _context.WSMY686.Where(p => p.Activo == true).FirstOrDefault();
                if (periodo != null)
                {
                    pCProceso = _context.PCProceso.FirstOrDefault();
                    if (pCProceso == null)
                    {

                        newpCProceso.Culminado = true;
                        newpCProceso.Iniciado = true;
                        newpCProceso.PeriodoDesde = $"{periodo.Desde.Day.ToString()}/{periodo.Desde.Month.ToString()}/{periodo.Desde.Year.ToString()}";
                        newpCProceso.PeriodoHasta = $"{periodo.Hasta.Day.ToString()}/{periodo.Hasta.Month.ToString()}/{periodo.Hasta.Year.ToString()}";
                        newpCProceso.RegistrosProcesados = 0;
                        newpCProceso.TotalReg = 0;
                        newpCProceso.FechaRegistro = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}"; 
                        newpCProceso.UsuarioRegistro = "SYSTEM";
                        newpCProceso.Desde = $"{periodo.Desde.Month.ToString("D2")}/01/{periodo.Desde.Year.ToString()}";
                        newpCProceso.Hasta = $"{periodo.Hasta.Month.ToString("D2")}/{periodo.Hasta.Day.ToString("D2")}/{periodo.Hasta.Year.ToString()}";
                        newpCProceso.Accion = 0;
                        string[] _desde = newpCProceso.PeriodoDesde.Split("/");
                        string diadesde = "00" + _desde[0];
                        string mesdesde = "00" + _desde[1]; ;
                        string añodesde = _desde[2];
                        string periodoDesde = $"{Right(mesdesde, 2)}/{Right(diadesde, 2)}/{añodesde}";

                        string[] _hasta = newpCProceso.PeriodoHasta.Split("/");
                        string diahasta = "00" + _hasta[0];
                        string meshasta = "00" + _hasta[1];
                        string añodhasta = _hasta[2];
                        string periodoHasta = $"{Right(meshasta, 2)}/{Right(diahasta, 2)}/{añodhasta}";

                        pCProceso.PeriodoCerrado = _context.PCHistorico.Any(e => e.PeriodoDesde == periodoDesde && e.PeriodoHasta == periodoHasta);
                        newpCProceso.IdPeriodo = periodo.ID;


                        _context.PCProceso.Add(newpCProceso);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        pCProceso.PeriodoDesde = $"{periodo.Desde.Day.ToString()}/{periodo.Desde.Month.ToString()}/{periodo.Desde.Year.ToString()}";
                        pCProceso.PeriodoHasta = $"{periodo.Hasta.Day.ToString()}/{periodo.Hasta.Month.ToString()}/{periodo.Hasta.Year.ToString()}";
                        pCProceso.Desde = $"{periodo.Desde.Month.ToString("D2")}/01/{periodo.Desde.Year.ToString()}";
                        pCProceso.Hasta = $"{periodo.Hasta.Month.ToString("D2")}/{periodo.Hasta.Day.ToString("D2")}/{periodo.Hasta.Year.ToString()}";

                        string[] _desde = pCProceso.PeriodoDesde.Split("/");
                        string diadesde = "00" + _desde[0];
                        string mesdesde = "00" + _desde[1]; ;
                        string añodesde = _desde[2];
                        string periodoDesde = $"{Right(mesdesde, 2)}/{Right(diadesde, 2)}/{añodesde}";

                        string[] _hasta = pCProceso.PeriodoHasta.Split("/");
                        string diahasta = "00" + _hasta[0];
                        string meshasta = "00" + _hasta[1];
                        string añodhasta = _hasta[2];
                        string periodoHasta = $"{Right(meshasta, 2)}/{Right(diahasta, 2)}/{añodhasta}";

                        pCProceso.PeriodoCerrado = _context.PCHistorico.Any(e => e.PeriodoDesde == periodoDesde && e.PeriodoHasta == periodoHasta);
                        pCProceso.IdPeriodo = periodo.ID;


                        _context.Entry(pCProceso).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw;
            }
           
           

            return await _context.PCProceso.ToListAsync();





        }

        // PUT: api/Calculo/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPCProceso(int id, PCProceso pCProceso)
        {
            if (id != pCProceso.Id)
            {
                return BadRequest();
            }

            _context.Entry(pCProceso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PCProcesoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool PCProcesoExists(int id)
        {
            return _context.PCProceso.Any(e => e.Id == id);
        }
        public string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }

    }
}