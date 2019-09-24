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
    public class TemporalController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public TemporalController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/Temporal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCTemporal>>> GetTemporal()
        {



            return await _context.PCTemporal.ToListAsync();


        }

        // GET: api/Temporal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PCResumenComisionTemporal>>> GetTemporalVendedor(int id)
       {
            var resumen = await _context.PCResumenComisionTemporal.Where(r=>r.CodigoOficina==id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;


        }
        // GET: api/Temporal/GetTemporalResumen
        [HttpGet("GetTemporalResumen")]
        public async Task<ActionResult<IEnumerable<PCResumenComisionTemporal>>> GetTemporalResumen()
        {
            var resumen = await _context.PCResumenComisionTemporal.ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }
        // GET: api/Temporal/5
        [HttpGet("GetTemporalVendedorDetalle/{id}")]
        public async Task<ActionResult<IEnumerable<PCTemporal>>> GetTemporalVendedorDetalle(string id)
        {
            var resumen = await _context.PCTemporal.Where(r => r.IdVendedor == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }

        // GET: api/Temporal/5
        [HttpGet("GetResumenVendedorDetalle/{id}")]
        public async Task<ActionResult<IEnumerable<PCResumenComisionTemporal>>> GetResumenVendedorDetalle(int id)
        {
            var resumen = await _context.PCResumenComisionTemporal.Where(r => r.Id == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }


        // GET: api/Historico/5
        [HttpGet("GetTemporalRecibo/{id}")]
        public async Task<ActionResult<IEnumerable<PCTemporal>>> GetTemporalRecibo(string id)
        {
            var resumen = await _context.PCTemporal.Where(r => r.DocumentoString == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }
        // GET: api/Temporal/5
        [HttpGet("GetTemporalOrden/{id}")]
        public async Task<ActionResult<IEnumerable<PCTemporal>>> GetTemporalOrden(string id)
        {
            var resumen = await _context.PCTemporal.Where(r => r.OrdenString == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }

    }
}