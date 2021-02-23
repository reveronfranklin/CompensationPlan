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
    public class ResumenComisionHistoricoController : ControllerBase
    {
        private readonly CompensationDbContext _context;

        //Controlador
        public ResumenComisionHistoricoController(CompensationDbContext context)
        {
            _context = context;
        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCResumenComisionHistorico>>> GetResumenComisionHistorico()
        {
            var pcResumenComisionHistorico = await _context.PCResumenComisionHistorico.ToListAsync();
                        
            if (pcResumenComisionHistorico == null)
            {
                return NotFound();
            }

            return pcResumenComisionHistorico;
        }

        // GET: 
        [HttpGet("{IdPeriodo}")]
        public async Task<ActionResult<IEnumerable<PCResumenComisionHistorico>>> GetResumenComisionHistoricoPoroficinaid(int IdPeriodo)
        {
            var pcResumenComisionHistorico = await _context.PCResumenComisionHistorico.Where(e => e.IdPeriodo == IdPeriodo).ToListAsync();
                       
            if (pcResumenComisionHistorico == null)
            {
                return NotFound();
            }

            return pcResumenComisionHistorico;
        }                
    }
}