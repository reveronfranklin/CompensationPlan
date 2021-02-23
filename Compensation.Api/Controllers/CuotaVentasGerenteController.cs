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
    public class CuotaVentasGerenteController : ControllerBase
    {
        private readonly CompensationDbContext _context;

        //Controlador
        public CuotaVentasGerenteController(CompensationDbContext context)
        {
            _context = context;
        }
              
        // GET: 
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PCCuotaVentasGerente>>> CuotaVentasGerente(string id)
        {
            var pcCuotaVentasGerente = await _context.PCCuotaVentasGerente.Where(e => e.Gerente == id).ToListAsync();
            
            if (pcCuotaVentasGerente == null)
            {
                return NotFound();
            }

            return pcCuotaVentasGerente;
        }
        
        private bool CuotaVentasExists(int id)
        {
            return _context.PCCuotaVentasGerente.Any(e => e.Id == id);
        }
    }
}