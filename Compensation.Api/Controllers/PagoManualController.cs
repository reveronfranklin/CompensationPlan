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
    public class PagoManualController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public PagoManualController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/PagoManual
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WSMY685>>> GetPagoManual()
        {



            return await _context.WSMY685.Where(p=>p.FlagPagado==false).ToListAsync();


        }

        // GET: api/PagoManual/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WSMY685>> GetPagoManual(int id)
        {
            var pagoManual = await _context.WSMY685.FindAsync(id);

            if (pagoManual == null)
            {
                return NotFound();
            }

            return pagoManual;
        }



        // POST: api/PAgoManual
        [HttpPost]
        public async Task<ActionResult<WSMY685>> PostFlatComision(WSMY685 pagoManual)
        {
           
            int IdSubcategoria = 0;
            var producto = _context.PCProducto.Where(p => p.Producto == pagoManual.Producto).FirstOrDefault();
            if (producto!=null)
            {
                IdSubcategoria = producto.IdSubcategoria;
            }
            var QryFicha = _context.WSMY693.Where(w=>w.IdSubCategoria== IdSubcategoria).FirstOrDefault();

            if (QryFicha != null && pagoManual.MontoGteProducto != 0)
            {
                pagoManual.FichaGteProducto = Convert.ToInt64(QryFicha.Ficha);
            }
            else
            {
                pagoManual.FichaGteProducto = 0;
            }
            try
            {
                _context.WSMY685.Add(pagoManual);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var a = e.Message.ToString();
                throw;
            }
           

            return CreatedAtAction("GetPagoManual", new { id = pagoManual.IdPago }, pagoManual);
        }


        // PUT: api/PagoManual/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlatComision(int id, WSMY685 pagoManual)
        {
            if (id != pagoManual.IdPago)
            {
                return BadRequest();
            }

            int IdSubcategoria = 0;
            var producto = _context.PCProducto.Where(p => p.Producto == pagoManual.Producto).FirstOrDefault();
            if (producto != null)
            {
                IdSubcategoria = producto.IdSubcategoria;
            }
            var QryFicha = _context.WSMY693.Where(w => w.IdSubCategoria == IdSubcategoria).FirstOrDefault();

            if (QryFicha != null && pagoManual.MontoGteProducto != 0)
            {
                pagoManual.FichaGteProducto = Convert.ToInt64(QryFicha.Ficha);
            }
            else
            {
                pagoManual.FichaGteProducto = 0;
            }



            _context.Entry(pagoManual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoManualExists(id))
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

        // DELETE: api/PagoManual/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WSMY685>> Delete(long id)
        {
            var pagoManual = await _context.WSMY685.Where(C=>C.IdPago==id).FirstAsync();
            if (pagoManual == null)
            {
                return NotFound();
            }


            _context.WSMY685.Remove(pagoManual);
            await _context.SaveChangesAsync();

            return pagoManual;
        }



        private bool PagoManualExists(int id)
        {
            return _context.WSMY685.Any(e => e.IdPago == id);
        }



    }
}