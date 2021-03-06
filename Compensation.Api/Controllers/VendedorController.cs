﻿using System;
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
    public class VendedorController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public VendedorController(CompensationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCVendedor>>> Vendedor()
        {
            _context.Database.ExecuteSqlCommand("PCSpVendedor @p0", "");

            return await _context.PCVendedor.Where(v=>v.Activo==true).ToListAsync();


        }

        // GET: api/Vendedor
        [HttpGet("GetGerente")]
        public async Task<ActionResult<IEnumerable<PCVendedor>>> GetGerente()
        {
            _context.Database.ExecuteSqlCommand("PCSpVendedor @p0", "");

            return await _context.PCVendedor.Where(v => v.Activo == true && v.Gerente== true).ToListAsync();


        }

        // GET: 
        [HttpGet("{id}")]
        public async Task<ActionResult<PCVendedor>> GetVendedorById(string id)
        {
            var vendedor = _context.PCVendedor.Where(h => h.IdVendedor == id).FirstOrDefault();
                        
            return vendedor;
        }

    }
}