using Compensaction.Share;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompensationPlan.Calculo
{

    public class LocalDbContext : DbContext
    {
        public DbSet<PCFlatComision> PCFlatComision { get; set; }
        public DbSet<WSMY436> WSMY436 { get; set; }
        public DbSet<WSMY437> WSMY437 { get; set; }
        public DbSet<PCAñoMesOrden> PCAñoMesOrden { get; set; }
        public DbSet<PCCuotaVentas> PCCuotaVentas { get; set; }
        public DbSet<PCRangoCumplimientoCuotaGeneral> PCRangoCumplimientoCuotaGeneral { get; set; }
        public DbSet<PCPorcCantidadCategoriasCubiertas> PCPorcCantidadCategoriasCubiertas { get; set; }

        public DbSet<PCProducto> PCProducto { get; set; }
        public DbSet<PCComisionesTemporal> PCComisionesTemporal { get; set; }
        public DbSet<PCProceso> PCProceso { get; set; }
        public DbSet<PCTipoPago> PCTipoPago { get; set; }
        public DbSet<PCTemporal> PCTemporal { get; set; }
        public DbSet<PCHistorico> PCHistorico { get; set; }
        public DbSet<PCSysFile> PCSysfile { get; set; }
        public DbSet<WSMY685> WSMY685 { get; set; }
        public DbSet<PCOficina> PCOficina { get; set; }
        public DbSet<PCVendedor> PCVendedor { get; set; }
        public DbSet<PCResumenComisionTemporal> PCResumenComisionTemporal { get; set; }
        public DbSet<PCTasaAñoMes> PCTasaAñoMes { get; set; }
        public DbSet<PCResumenOficinaTemporal> PCResumenOficinaTemporal { get; set; }
        public DbSet<PCProductoCuota> PCProductoCuota { get; set; }
        public DbSet<PCCuotaVentasGerente> PCCuotaVentasGerente { get; set; }
        public DbSet<PCFlatComisionGerente> PCFlatComisionGerente { get; set; }
        public DbSet<PCCliente> PCCliente { get; set; }
        public DbSet<PCOrdenesPignoradas> PCOrdenesPignoradas { get; set; }
        public DbSet<WSMY693> WSMY693 { get; set; }
        public DbSet<PCResumenOficinaHistorico> PCResumenOficinaHistorico { get; set; }
        public DbSet<PCResumenComisionHistorico> PCResumenComisionHistorico { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=PC;User Id=userweb;Password=userweb2003;MultipleActiveResultSets=true",
            opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
        }
    }
}
