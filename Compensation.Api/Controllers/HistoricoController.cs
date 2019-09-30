using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compensaction.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compensation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly CompensationDbContext _context;

        public HistoricoController(CompensationDbContext context)
        {
            _context = context;
        }

        // GET: api/Periodo/5
        [HttpGet("{id}/{vendedor}")]
        public async Task<ActionResult<IEnumerable<PCHistorico>>> GetHistorico(int id,string vendedor)
        {
            var historico = await _context.PCHistorico.Where(h => h.IdPeriodo == id && h.IdVendedor==vendedor).OrderBy(h=>h.Documento).ToListAsync();

            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }


        // GET: api/Historico/5
        [HttpGet("GetHistoricoOrden/{id}")]
        public async Task<ActionResult<IEnumerable<PCHistorico>>> GetHistoricoOrden(string id)
        {
            var resumen = await _context.PCHistorico.Where(r => r.OrdenString == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }

        // GET: api/Historico/5
        [HttpGet("GetHistoricoRecibo/{id}")]
        public async Task<ActionResult<IEnumerable<PCHistorico>>> GetHistoricoRecibo(string id)
        {
            var resumen = await _context.PCHistorico.Where(r => r.DocumentoString == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }

    }
}