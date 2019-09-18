using Compensaction.Share;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Compensation.Api.Bussines
{
    public class Calculo
    {

        private readonly CompensationDbContext _context;

        public Calculo(CompensationDbContext context)
        {
            _context = context;
        }


        #region MetodosPincipales
        //***********Tabla Uno - Comision Flat ****************************************************
        public ComisionFlatView ComisionFlat(int IdSubcategoria, decimal monto)
        {
            decimal Porcentaje = 0;
            decimal BsComisionDef = 0;
            ComisionFlatView comisionFlat = new ComisionFlatView();
            Porcentaje = PorcFlat(IdSubcategoria);

            BsComisionDef = (monto * Porcentaje) / 100;
            comisionFlat.Comision = BsComisionDef;
            comisionFlat.Porcentaje = Porcentaje;

            return comisionFlat;

        }

        //***********Tabla Dos - ComisionRangoCumplimientoCuotaGeneral*****************************
        public RangoCumplimientoCuotaGeneralView ComisionCumplimientoCuotaGeneral(int Año, int Mes, string Vendedor, decimal BsComisionDef)
        {


            decimal ComisionRangoCumplimientoCuotaGeneral = 0;
            decimal TotalCuota = 0;
            decimal Totalventa = 0;
            RangoCumplimientoCuotaGeneralView rangoCumplimientoCuotaGeneralView = new RangoCumplimientoCuotaGeneralView();
            List<PCCuotaVentas> pCCuotaVentas = new List<PCCuotaVentas>();
            pCCuotaVentas = _context.PCCuotaVentas.Where(c => c.Año == Año && c.Mes == Mes && c.Vendedor == Vendedor.Trim()).ToList();
            if (pCCuotaVentas != null)
            {
                foreach (var cuota in pCCuotaVentas)
                {
                    TotalCuota = TotalCuota + cuota.Cuota;
                    Totalventa = Totalventa + cuota.Venta;
                }
            }

            decimal PorcCumplimiento = 0;

            if (TotalCuota > 0)
            {
                PorcCumplimiento = (Totalventa * 100) / TotalCuota;

            }
           

            decimal RangoCumplimientoCuotaGeneral = 0;

            RangoCumplimientoCuotaGeneral = PorcRangoCumplimientoCuotaGeneral(PorcCumplimiento);

            ComisionRangoCumplimientoCuotaGeneral = (BsComisionDef * RangoCumplimientoCuotaGeneral) / 100;

            rangoCumplimientoCuotaGeneralView.Porcentaje = RangoCumplimientoCuotaGeneral;
            rangoCumplimientoCuotaGeneralView.Comision = ComisionRangoCumplimientoCuotaGeneral;
            rangoCumplimientoCuotaGeneralView.TotalCuota = TotalCuota;
            rangoCumplimientoCuotaGeneralView.TotalVenta = Totalventa;
            return rangoCumplimientoCuotaGeneralView;

        }

        //***********Tabla Tres - Cantidad de Cuotas Cumplidas **** 
        public ComisionCantidadCuotasCumplidasView ComisionCantidadCuotasCumplidas(int Año, int Mes, string Vendedor, decimal BsComisionDef)
        {
            decimal Comision = 0;
            decimal Porcentaje = 0;
            int CantidadCuotasCumplidas = 0;
            decimal cumplimiento = (decimal)50.01;
            ComisionCantidadCuotasCumplidasView comisionCantidadCuotasCumplidasView = new ComisionCantidadCuotasCumplidasView();
            List<PCCuotaVentas> pCCuotaVentas = new List<PCCuotaVentas>();

            PCPorcCantidadCategoriasCubiertas pCPorcCantidadCategoriasCubiertas = new PCPorcCantidadCategoriasCubiertas();

            pCCuotaVentas = _context.PCCuotaVentas.Where(c => c.Año == Año && c.Mes == Mes && c.Vendedor == Vendedor && c.PorcCumplimiento >= cumplimiento).ToList();
            if (pCCuotaVentas != null)
            {
                CantidadCuotasCumplidas = pCCuotaVentas.Count;
                pCPorcCantidadCategoriasCubiertas = _context.PCPorcCantidadCategoriasCubiertas.Where(c => c.Cantidad <= CantidadCuotasCumplidas).OrderByDescending(c => c.Cantidad).FirstOrDefault();
                if (pCPorcCantidadCategoriasCubiertas != null)
                {
                    Comision = (BsComisionDef * pCPorcCantidadCategoriasCubiertas.Porcentaje) / 100;
                    Porcentaje=pCPorcCantidadCategoriasCubiertas.Porcentaje;
                }
            }

            comisionCantidadCuotasCumplidasView.Comision = Comision;
            comisionCantidadCuotasCumplidasView.Porcentaje= Porcentaje;
            comisionCantidadCuotasCumplidasView.CantidadCuotasCumplidas = CantidadCuotasCumplidas;


            return comisionCantidadCuotasCumplidasView;



        }

        

        //************Calcula Recibo Por id de Tabla ****************************************************
        public void CalcularRecibo(decimal IdTabla)
        {
            
            int idSubcategoria = 0;
            PCComisionesTemporal pCComisionesTemporal = new PCComisionesTemporal();
            pCComisionesTemporal = _context.PCComisionesTemporal.Find(IdTabla);
            if (pCComisionesTemporal != null)
            {

                var añoMesOrden = AñoOrden(pCComisionesTemporal.Orden.ToString());

                idSubcategoria = SubcategoriaProducto(pCComisionesTemporal.Producto.Trim());
                var comisionFlat = ComisionFlat(idSubcategoria, pCComisionesTemporal.MontoReal);
                pCComisionesTemporal.BsComision = comisionFlat.Comision;
                pCComisionesTemporal.PorcFlat = comisionFlat.Porcentaje;

                var comisionCumplimientoCuotaGeneral = ComisionCumplimientoCuotaGeneral(añoMesOrden.Año, añoMesOrden.Mes, pCComisionesTemporal.IdVendedor.Trim(), pCComisionesTemporal.BsComision);
                pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneral = comisionCumplimientoCuotaGeneral.Comision;
                pCComisionesTemporal.PorcRangoCumplimientoCuotaGeneral = comisionCumplimientoCuotaGeneral.Porcentaje;
                pCComisionesTemporal.TotalCuotaMes = comisionCumplimientoCuotaGeneral.TotalCuota;
                pCComisionesTemporal.TotalVentasMes = comisionCumplimientoCuotaGeneral.TotalVenta;

                var comisionCantidadCuotasCumplidas = ComisionCantidadCuotasCumplidas(añoMesOrden.Año, añoMesOrden.Mes, pCComisionesTemporal.IdVendedor, pCComisionesTemporal.BsComision);
                pCComisionesTemporal.PorcCantidadCuotasCumplidas = comisionCantidadCuotasCumplidas.Porcentaje;
                pCComisionesTemporal.ComisionCantidadCuotasCumplidas = comisionCantidadCuotasCumplidas.Comision;
                pCComisionesTemporal.CantidadCuotasCumplidas = comisionCantidadCuotasCumplidas.CantidadCuotasCumplidas;


                _context.Entry(pCComisionesTemporal).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }


        #endregion


        #region MetodosAuxiliares
        public decimal PorcRangoCumplimientoCuotaGeneral(decimal PorcCumplimiento)
        {

            decimal porc = 0;

            PCRangoCumplimientoCuotaGeneral pCRangoCumplimientoCuotaGeneral = new PCRangoCumplimientoCuotaGeneral();
            pCRangoCumplimientoCuotaGeneral = _context.PCRangoCumplimientoCuotaGeneral.Where(r => PorcCumplimiento >= r.Desde && PorcCumplimiento <= r.Hasta).FirstOrDefault();
            if (pCRangoCumplimientoCuotaGeneral != null)
            {
                porc = pCRangoCumplimientoCuotaGeneral.Porcentaje;
            }
            return porc;

        }

        public decimal PorcFlat(int IdSubcategoria)
        {

            int IdTablaFlatComision = 0;
            decimal porcFlat = 0;

            //WSMY437 Tabla de Subcategoria de productos
            WSMY437 _WSMY437 = new WSMY437();

            PCFlatComision pCFlatComision = new PCFlatComision();

            _WSMY437 = _context.WSMY437.Find(IdSubcategoria);

            if (_WSMY437 != null)
            {
                IdTablaFlatComision = (int)_WSMY437.IdTablaFlatComision;

                pCFlatComision = _context.PCFlatComision.Find(IdTablaFlatComision);

                if (pCFlatComision != null)
                {
                    porcFlat = pCFlatComision.Porcentaje;
                }
            }


            return porcFlat;

        }

        public PCAñoMesOrden AñoOrden(string Orden)
        {



            _context.Database.ExecuteSqlCommand("PCAñoOrden @p0", Orden);

            var pcAñoMesOrden = _context.PCAñoMesOrden.Where(o => o.Orden == Orden).FirstOrDefault();
            if (pcAñoMesOrden == null)
            {
                pcAñoMesOrden.Año = 0;
                pcAñoMesOrden.Mes = 0;
                pcAñoMesOrden.Orden = Orden;
            }


            return pcAñoMesOrden;

        }
        public int SubcategoriaProducto(string Producto)
        {


            int Subcategoria = 0;
            _context.Database.ExecuteSqlCommand("PCSpProducto @p0", Producto.Trim());

            var pcProducto = _context.PCProducto.Where(o => o.Producto == Producto.Trim()).FirstOrDefault();
            if (pcProducto != null)
            {
                Subcategoria = pcProducto.IdSubcategoria;
            }


            return Subcategoria;

        }
       
        #endregion

    }
}
