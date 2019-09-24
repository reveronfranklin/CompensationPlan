using Compensaction.Share;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompensationPlan.Calculo.Bussines
{
    public class CalcularComision
    {
        private readonly LocalDbContext _context;

        public CalcularComision(LocalDbContext context)
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

        public ComisionFlatView ComisionFlatGerente(int IdSubcategoria, decimal monto, string gerente)
        {
            decimal Porcentaje = 0;
            decimal BsComisionDef = 0;
            ComisionFlatView comisionFlat = new ComisionFlatView();
            Porcentaje = PorcFlatGerente(IdSubcategoria, gerente);

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

        public RangoCumplimientoCuotaGeneralView ComisionCumplimientoCuotaGeneralGerente(int Año, int Mes, string Vendedor, decimal BsComisionDef)
        {


            decimal ComisionRangoCumplimientoCuotaGeneral = 0;
            decimal TotalCuota = 0;
            decimal Totalventa = 0;
            string Supervisor = "";

            Supervisor = Gerente(Vendedor.Trim());
            RangoCumplimientoCuotaGeneralView rangoCumplimientoCuotaGeneralView = new RangoCumplimientoCuotaGeneralView();
            List<PCCuotaVentas> pCCuotaVentas = new List<PCCuotaVentas>();
            pCCuotaVentas = _context.PCCuotaVentas.Where(c => c.Año == Año && c.Mes == Mes && c.Supervisor == Supervisor.Trim()).ToList();
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

            RangoCumplimientoCuotaGeneral = PorcRangoCumplimientoCuotaGeneralGerente(PorcCumplimiento);

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
            decimal cumplimiento = 0;

            PCSysFile pCSysfile = new PCSysFile();
            pCSysfile = _context.PCSysfile.FirstOrDefault();
            cumplimiento = pCSysfile.PorcCunplimiento;
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
                    Porcentaje = pCPorcCantidadCategoriasCubiertas.Porcentaje;
                }
            }

            comisionCantidadCuotasCumplidasView.Comision = Comision;
            comisionCantidadCuotasCumplidasView.Porcentaje = Porcentaje;
            comisionCantidadCuotasCumplidasView.CantidadCuotasCumplidas = CantidadCuotasCumplidas;


            return comisionCantidadCuotasCumplidasView;



        }

        public ComisionCantidadCuotasCumplidasView ComisionCantidadCuotasCumplidasGerente(int Año, int Mes, string Vendedor, decimal BsComisionDef)
        {
            decimal Comision = 0;
            decimal Porcentaje = 0;
            int CantidadCuotasCumplidas = 0;
            decimal cumplimiento = 0;

            PCSysFile pCSysfile = new PCSysFile();
            pCSysfile = _context.PCSysfile.FirstOrDefault();
            cumplimiento = pCSysfile.PorcCunplimiento;

            string Supervisor = "";
            ComisionCantidadCuotasCumplidasView comisionCantidadCuotasCumplidasView = new ComisionCantidadCuotasCumplidasView();
            List<PCCuotaVentasGerente> pCCuotaVentas = new List<PCCuotaVentasGerente>();

            PCPorcCantidadCategoriasCubiertas pCPorcCantidadCategoriasCubiertas = new PCPorcCantidadCategoriasCubiertas();
            Supervisor = Gerente(Vendedor.Trim());
            pCCuotaVentas = _context.PCCuotaVentasGerente.Where(c => c.Año == Año && c.Mes == Mes && c.Gerente == Supervisor && c.PorcCumplimiento >= cumplimiento).ToList();
            if (pCCuotaVentas != null)
            {
                CantidadCuotasCumplidas = pCCuotaVentas.Count;
                pCPorcCantidadCategoriasCubiertas = _context.PCPorcCantidadCategoriasCubiertas.Where(c => c.Cantidad <= CantidadCuotasCumplidas).OrderByDescending(c => c.Cantidad).FirstOrDefault();
                if (pCPorcCantidadCategoriasCubiertas != null)
                {
                    Comision = (BsComisionDef * pCPorcCantidadCategoriasCubiertas.PorcentajeGerente) / 100;
                    Porcentaje = pCPorcCantidadCategoriasCubiertas.PorcentajeGerente;
                }
            }

            comisionCantidadCuotasCumplidasView.Comision = Comision;
            comisionCantidadCuotasCumplidasView.Porcentaje = Porcentaje;
            comisionCantidadCuotasCumplidasView.CantidadCuotasCumplidas = CantidadCuotasCumplidas;


            return comisionCantidadCuotasCumplidasView;



        }


        //************Calcula Recibo Por id de Tabla ****************************************************
        public void CalcularRecibo(decimal IdTabla)
        {

            int idSubcategoria = 0;
            PCComisionesTemporal pCComisionesTemporal = new PCComisionesTemporal();
            PCTipoPago pCTipoPago = new PCTipoPago();



            pCComisionesTemporal = _context.PCComisionesTemporal.Find(IdTabla);
            if (pCComisionesTemporal != null)
            {

                if (pCComisionesTemporal.Documento == 253810)
                {
                    var a = 1;
                    Console.WriteLine(a);
                }

                pCTipoPago = _context.PCTipoPago.Where(t => t.Id == pCComisionesTemporal.IdTipoPago).FirstOrDefault();
                if (pCTipoPago.FlagCalcular)
                {

                    var añoMesOrden = AñoOrden(pCComisionesTemporal.Orden.ToString());

                    idSubcategoria = SubcategoriaProducto(pCComisionesTemporal.Producto.Trim());
                    //Comision Flat vendedor
                    var comisionFlat = ComisionFlat(idSubcategoria, pCComisionesTemporal.MontoReal);
                    pCComisionesTemporal.BsComision = comisionFlat.Comision;
                    pCComisionesTemporal.PorcFlat = comisionFlat.Porcentaje;

                    //Comision Flat Gerente
                    var comisionFlatGerente = ComisionFlatGerente(idSubcategoria, pCComisionesTemporal.MontoReal, Gerente(pCComisionesTemporal.IdVendedor));
                    pCComisionesTemporal.BsComisionGrte = comisionFlatGerente.Comision;
                    pCComisionesTemporal.PorcFlatGerente = comisionFlatGerente.Porcentaje;


                    //Cumplimiento Cuota General Vendedor
                    var comisionCumplimientoCuotaGeneral = ComisionCumplimientoCuotaGeneral(añoMesOrden.Año, añoMesOrden.Mes, pCComisionesTemporal.IdVendedor.Trim(), pCComisionesTemporal.BsComision);
                    pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneral = comisionCumplimientoCuotaGeneral.Comision;
                    pCComisionesTemporal.PorcRangoCumplimientoCuotaGeneral = comisionCumplimientoCuotaGeneral.Porcentaje;
                    pCComisionesTemporal.TotalCuotaMes = comisionCumplimientoCuotaGeneral.TotalCuota;
                    pCComisionesTemporal.TotalVentasMes = comisionCumplimientoCuotaGeneral.TotalVenta;

                    //Cumplimiento Cuota General Gerente
                    var comisionCumplimientoCuotaGeneralGerente = ComisionCumplimientoCuotaGeneralGerente(añoMesOrden.Año, añoMesOrden.Mes, pCComisionesTemporal.IdVendedor.Trim(), pCComisionesTemporal.BsComisionGrte);
                    pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneralGerente = comisionCumplimientoCuotaGeneralGerente.Comision;
                    pCComisionesTemporal.PorcRangoCumplimientoCuotaGeneralGerente = comisionCumplimientoCuotaGeneralGerente.Porcentaje;
                    pCComisionesTemporal.TotalCuotaMesGerente = comisionCumplimientoCuotaGeneralGerente.TotalCuota;
                    pCComisionesTemporal.TotalVentasMesGerente = comisionCumplimientoCuotaGeneralGerente.TotalVenta;



                    //Cantidad Cuotas Cumplidas Vendedor
                    var comisionCantidadCuotasCumplidas = ComisionCantidadCuotasCumplidas(añoMesOrden.Año, añoMesOrden.Mes, pCComisionesTemporal.IdVendedor, pCComisionesTemporal.BsComision);
                    pCComisionesTemporal.PorcCantidadCuotasCumplidas = comisionCantidadCuotasCumplidas.Porcentaje;
                    pCComisionesTemporal.ComisionCantidadCuotasCumplidas = comisionCantidadCuotasCumplidas.Comision;
                    pCComisionesTemporal.CantidadCuotasCumplidas = comisionCantidadCuotasCumplidas.CantidadCuotasCumplidas;

                    //Cantidad Cuotas Cumplidas Gerente
                    var comisionCantidadCuotasCumplidasGerente = ComisionCantidadCuotasCumplidasGerente(añoMesOrden.Año, añoMesOrden.Mes, pCComisionesTemporal.IdVendedor, pCComisionesTemporal.BsComisionGrte);
                    pCComisionesTemporal.PorcCantidadCuotasCumplidasGerente = comisionCantidadCuotasCumplidasGerente.Porcentaje;
                    pCComisionesTemporal.ComisionCantidadCuotasCumplidasGerente = comisionCantidadCuotasCumplidasGerente.Comision;
                    pCComisionesTemporal.CantidadCuotasCumplidasGerente = comisionCantidadCuotasCumplidasGerente.CantidadCuotasCumplidas;



                    _context.Entry(pCComisionesTemporal).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }

            CreaDetalleTemporal(pCComisionesTemporal);

        }


        //********************Cierre del Periodo******************************

        public void Cierre()
        {
            List<PCTemporal> pCTemporal = new List<PCTemporal>();
            PCHistorico pCHistorico = new PCHistorico();

            var proceso = _context.PCProceso.FirstOrDefault();
            if (proceso != null)
            {

                pCTemporal = _context.PCTemporal.ToList();
                if (pCTemporal != null)
                {
                    int cant = 1;
                    foreach (var item in pCTemporal)
                    {
                        pCHistorico.Id = Guid.NewGuid().ToString();
                        pCHistorico.IdCliente = item.IdCliente;
                        pCHistorico.Transaccion = item.Transaccion;
                        pCHistorico.Documento = item.Documento;
                        pCHistorico.Linea = item.Linea;
                        pCHistorico.IdVendedor = item.IdVendedor;
                        pCHistorico.Orden = item.Orden;
                        pCHistorico.Producto = item.Producto;
                        pCHistorico.MontoReal = item.MontoReal;
                        pCHistorico.BsComision = item.BsComision;
                        pCHistorico.PorcFlat = item.PorcFlat;
                        pCHistorico.TotalVentasMes = item.TotalVentasMes;
                        pCHistorico.TotalCuotaMes = item.TotalCuotaMes;
                        pCHistorico.IdTipoPago = item.IdTipoPago;
                        pCHistorico.CantidadCuotasCumplidas = item.CantidadCuotasCumplidas;
                        pCHistorico.PeriodoDesde = item.PeriodoDesde;
                        pCHistorico.PeriodoHasta = item.PeriodoHasta;
                        pCHistorico.ComisionRangoCumplimientoCuotaGeneral = item.ComisionRangoCumplimientoCuotaGeneral;
                        pCHistorico.PorcRangoCumplimientoCuotaGeneral = item.PorcRangoCumplimientoCuotaGeneral;
                        pCHistorico.PorcCantidadCuotasCumplidas = item.PorcCantidadCuotasCumplidas;
                        pCHistorico.ComisionCantidadCuotasCumplidas = item.ComisionCantidadCuotasCumplidas;
                        pCHistorico.FechaRegistro = DateTime.UtcNow;
                        pCHistorico.DescripcionTipoPago = item.DescripcionTipoPago;
                        pCHistorico.MontoString = ToCurrencyString(item.BsComision);
                        pCHistorico.OrdenString = item.Orden.ToString();
                        pCHistorico.DocumentoString = item.Documento.ToString();
                        pCHistorico.MontoRealString = ToCurrencyString(item.MontoReal);
                        pCHistorico.IdPeriodo = item.IdPeriodo;

                        _context.PCHistorico.Add(pCHistorico);

                        proceso.RegistrosCerrados = cant++;

                        Console.WriteLine($"Cerrando el reg Nro:{item.Id}");

                        _context.SaveChanges();

                        //***** Marca el Pago Manual
                        if (item.Transaccion == "PM")
                        {
                            MarcaPagosManuales(item);
                        }



                    }

                }
            }


            _context.Database.ExecuteSqlCommand("PCLimpiaTemporal @p0", "");

        }


        #endregion


        #region MetodosAuxiliares
        public void extraerDatos(string desde, string hasta, string usuario)
        {

            _context.Database.ExecuteSqlCommand("PaNpcETL_CXC_Comisiones @p0, @p1,@p2", desde, hasta, usuario);


        }
        public void MarcaPagosManuales(PCTemporal pCTemporal)
        {

            WSMY685 pagoManual = new WSMY685();
            pagoManual = _context.WSMY685.Where(p => p.IdPago == pCTemporal.Documento).FirstOrDefault();
            if (pagoManual != null)
            {
                pagoManual.FlagPagado = true;
                pagoManual.FechaPagado = DateTime.UtcNow;
                _context.Entry(pagoManual).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public void CreaDetalleTemporal(PCComisionesTemporal pCComisionesTemporal)
        {
            //Pago Manual
            PCTemporal pCTemporalPM = new PCTemporal();
            //Comision Flat
            PCTemporal pCTemporalCF = new PCTemporal();
            //Comision Flat Cliente Nuevo
            PCTemporal pCTemporalCFNuevo = new PCTemporal();
            //Cumplimiento Cuota General
            PCTemporal pCTemporalCC = new PCTemporal();
            //Cantidad Cuotas Cumplidas
            PCTemporal pCTemporalCA = new PCTemporal();

            //Comision Flat Gerente
            PCTemporal pCTemporalFG = new PCTemporal();
            //Comision Flat Gerente Cliente Nevo
            PCTemporal pCTemporalFGNuevo = new PCTemporal();
            //Cumplimiento Cuota General Gerente
            PCTemporal pCTemporalCG = new PCTemporal();
            //CantidadCuotasCumplidasGerente
            PCTemporal pCTemporalCB = new PCTemporal();

            PCTemporal pCTemporal = new PCTemporal();
            PCTipoPago pCTipoPago = new PCTipoPago();
            PCTemporal apagar = new PCTemporal();

            var proceso = _context.PCProceso.FirstOrDefault();

            string[] _desde = proceso.PeriodoDesde.Split("/");
            string diadesde = "00" + _desde[0];
            string mesdesde = "00" + _desde[1]; ;
            string añodesde = _desde[2];
            string periodoDesde = $"{Right(mesdesde, 2)}/{Right(diadesde, 2)}/{añodesde}";

            string[] _hasta = proceso.PeriodoHasta.Split("/");
            string diahasta = "00" + _hasta[0];
            string meshasta = "00" + _hasta[1];
            string añodhasta = _hasta[2];
            string periodoHasta = $"{Right(meshasta, 2)}/{Right(diahasta, 2)}/{añodhasta}";

            bool clientePagaComisionDoble = ClientePagaComisionDoble(pCComisionesTemporal.IdCliente, (DateTime)GetfechaOrden(pCComisionesTemporal.Orden.ToString()));

            bool ordenPignorada = OrdenPignorada(pCComisionesTemporal.Orden.ToString());

            pCTipoPago = _context.PCTipoPago.Where(t => t.Id == pCComisionesTemporal.IdTipoPago).FirstOrDefault();
            if (pCTipoPago.FlagCalcular)
            {
                //Se Crea Registro de Comision Flat (TipoPagoEnum.ComisionFlat)
                pCTemporalCF.Id = Guid.NewGuid().ToString();
                pCTemporalCF.IdCliente = pCComisionesTemporal.IdCliente;
                pCTemporalCF.Transaccion = pCComisionesTemporal.Transaccion;
                pCTemporalCF.Documento = pCComisionesTemporal.Documento;
                pCTemporalCF.Linea = pCComisionesTemporal.Linea;
                pCTemporalCF.IdVendedor = pCComisionesTemporal.IdVendedor;
                pCTemporalCF.Orden = pCComisionesTemporal.Orden;
                pCTemporalCF.Producto = pCComisionesTemporal.Producto;
                pCTemporalCF.MontoReal = pCComisionesTemporal.MontoReal;
                pCTemporalCF.BsComision = pCComisionesTemporal.BsComision;
                pCTemporalCF.PorcFlat = pCComisionesTemporal.PorcFlat;
                pCTemporalCF.TotalVentasMes = pCComisionesTemporal.TotalVentasMes;
                pCTemporalCF.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMes;
                pCTemporalCF.IdTipoPago = (int)TipoPagoEnum.ComisionFlat;
                pCTemporalCF.PeriodoDesde = periodoDesde;
                pCTemporalCF.PeriodoHasta = periodoHasta;
                pCTemporalCF.ComisionRangoCumplimientoCuotaGeneral = 0;
                pCTemporalCF.PorcRangoCumplimientoCuotaGeneral = 0;
                pCTemporalCF.CantidadCuotasCumplidas = 0;
                pCTemporalCF.PorcCantidadCuotasCumplidas = 0;
                pCTemporalCF.ComisionCantidadCuotasCumplidas = 0;
                pCTemporalCF.FechaRegistro = DateTime.UtcNow;
                pCTemporalCF.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalCF.IdTipoPago);
                pCTemporalCF.MontoString = ToCurrencyString(pCTemporalCF.BsComision);
                pCTemporalCF.OrdenString = pCTemporalCF.Orden.ToString();
                pCTemporalCF.DocumentoString = pCTemporalCF.Documento.ToString();
                pCTemporalCF.MontoRealString = ToCurrencyString(pCTemporalCF.MontoReal);
                pCTemporalCF.IdPeriodo = proceso.IdPeriodo;
                if (!ExisteHistorico(pCTemporalCF))
                {
                    _context.PCTemporal.Add(pCTemporalCF);
                }

                //Se Crea Registro de Comision Flat Clente Nuevo o Reactivado(TipoPagoEnum.ComisionFlatClienteNuevoReactivado)
                if (clientePagaComisionDoble && ordenPignorada == false)
                {
                    pCTemporalCFNuevo.Id = Guid.NewGuid().ToString();
                    pCTemporalCFNuevo.IdCliente = pCComisionesTemporal.IdCliente;
                    pCTemporalCFNuevo.Transaccion = pCComisionesTemporal.Transaccion;
                    pCTemporalCFNuevo.Documento = pCComisionesTemporal.Documento;
                    pCTemporalCFNuevo.Linea = pCComisionesTemporal.Linea;
                    pCTemporalCFNuevo.IdVendedor = pCComisionesTemporal.IdVendedor;
                    pCTemporalCFNuevo.Orden = pCComisionesTemporal.Orden;
                    pCTemporalCFNuevo.Producto = pCComisionesTemporal.Producto;
                    pCTemporalCFNuevo.MontoReal = pCComisionesTemporal.MontoReal;
                    pCTemporalCFNuevo.BsComision = pCComisionesTemporal.BsComision;
                    pCTemporalCFNuevo.PorcFlat = pCComisionesTemporal.PorcFlat;
                    pCTemporalCFNuevo.TotalVentasMes = pCComisionesTemporal.TotalVentasMes;
                    pCTemporalCFNuevo.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMes;
                    pCTemporalCFNuevo.IdTipoPago = (int)TipoPagoEnum.ComisionFlatClienteNuevoReactivado;
                    pCTemporalCFNuevo.PeriodoDesde = periodoDesde;
                    pCTemporalCFNuevo.PeriodoHasta = periodoHasta;
                    pCTemporalCFNuevo.ComisionRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalCFNuevo.PorcRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalCFNuevo.CantidadCuotasCumplidas = 0;
                    pCTemporalCFNuevo.PorcCantidadCuotasCumplidas = 0;
                    pCTemporalCFNuevo.ComisionCantidadCuotasCumplidas = 0;
                    pCTemporalCFNuevo.FechaRegistro = DateTime.UtcNow;
                    pCTemporalCFNuevo.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalCFNuevo.IdTipoPago);
                    pCTemporalCFNuevo.MontoString = ToCurrencyString(pCTemporalCFNuevo.BsComision);
                    pCTemporalCFNuevo.OrdenString = pCTemporalCFNuevo.Orden.ToString();
                    pCTemporalCFNuevo.DocumentoString = pCTemporalCFNuevo.Documento.ToString();
                    pCTemporalCFNuevo.MontoRealString = ToCurrencyString(pCTemporalCFNuevo.MontoReal);
                    pCTemporalCFNuevo.IdPeriodo = proceso.IdPeriodo;
                    if (!ExisteHistorico(pCTemporalCFNuevo))
                    {
                        _context.PCTemporal.Add(pCTemporalCFNuevo);
                    }
                }



                //Se Crea Registro de Comision Flat Gerente (TipoPagoEnum.ComisionFlatGerente)
                pCTemporalFG.Id = Guid.NewGuid().ToString();
                pCTemporalFG.IdCliente = pCComisionesTemporal.IdCliente;
                pCTemporalFG.Transaccion = pCComisionesTemporal.Transaccion;
                pCTemporalFG.Documento = pCComisionesTemporal.Documento;
                pCTemporalFG.Linea = pCComisionesTemporal.Linea;
                pCTemporalFG.IdVendedor = Gerente(pCComisionesTemporal.IdVendedor);
                pCTemporalFG.Orden = pCComisionesTemporal.Orden;
                pCTemporalFG.Producto = pCComisionesTemporal.Producto;
                pCTemporalFG.MontoReal = pCComisionesTemporal.MontoReal;
                pCTemporalFG.BsComision = pCComisionesTemporal.BsComisionGrte;
                pCTemporalFG.PorcFlat = pCComisionesTemporal.PorcFlatGerente;
                pCTemporalFG.TotalVentasMes = pCComisionesTemporal.TotalVentasMesGerente;
                pCTemporalFG.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMesGerente;
                pCTemporalFG.IdTipoPago = (int)TipoPagoEnum.ComisionFlatGerente;
                pCTemporalFG.PeriodoDesde = periodoDesde;
                pCTemporalFG.PeriodoHasta = periodoHasta;
                pCTemporalFG.ComisionRangoCumplimientoCuotaGeneral = 0;
                pCTemporalFG.PorcRangoCumplimientoCuotaGeneral = 0;
                pCTemporalFG.CantidadCuotasCumplidas = 0;
                pCTemporalCF.PorcCantidadCuotasCumplidas = 0;
                pCTemporalFG.ComisionCantidadCuotasCumplidas = 0;
                pCTemporalFG.FechaRegistro = DateTime.UtcNow;
                pCTemporalFG.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalFG.IdTipoPago);
                pCTemporalFG.MontoString = ToCurrencyString(pCTemporalFG.BsComision);
                pCTemporalFG.OrdenString = pCTemporalFG.Orden.ToString();
                pCTemporalFG.DocumentoString = pCTemporalFG.Documento.ToString();
                pCTemporalFG.MontoRealString = ToCurrencyString(pCTemporalFG.MontoReal);
                pCTemporalFG.IdPeriodo = proceso.IdPeriodo;
                if (!ExisteHistorico(pCTemporalFG))
                {
                    _context.PCTemporal.Add(pCTemporalFG);
                }

                //Se Crea Registro de Comision Flat Gerente Cliente nuevo(TipoPagoEnum.ComisionFlatClienteNuevoReactivadoGerente)
                if (clientePagaComisionDoble && ordenPignorada == false)
                {
                    pCTemporalFGNuevo.Id = Guid.NewGuid().ToString();
                    pCTemporalFGNuevo.IdCliente = pCComisionesTemporal.IdCliente;
                    pCTemporalFGNuevo.Transaccion = pCComisionesTemporal.Transaccion;
                    pCTemporalFGNuevo.Documento = pCComisionesTemporal.Documento;
                    pCTemporalFGNuevo.Linea = pCComisionesTemporal.Linea;
                    pCTemporalFGNuevo.IdVendedor = Gerente(pCComisionesTemporal.IdVendedor);
                    pCTemporalFGNuevo.Orden = pCComisionesTemporal.Orden;
                    pCTemporalFGNuevo.Producto = pCComisionesTemporal.Producto;
                    pCTemporalFGNuevo.MontoReal = pCComisionesTemporal.MontoReal;
                    pCTemporalFGNuevo.BsComision = pCComisionesTemporal.BsComisionGrte;
                    pCTemporalFGNuevo.PorcFlat = pCComisionesTemporal.PorcFlatGerente;
                    pCTemporalFGNuevo.TotalVentasMes = pCComisionesTemporal.TotalVentasMesGerente;
                    pCTemporalFGNuevo.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMesGerente;
                    pCTemporalFGNuevo.IdTipoPago = (int)TipoPagoEnum.ComisionFlatClienteNuevoReactivadoGerente;
                    pCTemporalFGNuevo.PeriodoDesde = periodoDesde;
                    pCTemporalFGNuevo.PeriodoHasta = periodoHasta;
                    pCTemporalFGNuevo.ComisionRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalFGNuevo.PorcRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalFGNuevo.CantidadCuotasCumplidas = 0;
                    pCTemporalFGNuevo.PorcCantidadCuotasCumplidas = 0;
                    pCTemporalFGNuevo.ComisionCantidadCuotasCumplidas = 0;
                    pCTemporalFGNuevo.FechaRegistro = DateTime.UtcNow;
                    pCTemporalFG.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalFG.IdTipoPago);
                    pCTemporalFGNuevo.MontoString = ToCurrencyString(pCTemporalFGNuevo.BsComision);
                    pCTemporalFGNuevo.OrdenString = pCTemporalFGNuevo.Orden.ToString();
                    pCTemporalFGNuevo.DocumentoString = pCTemporalFGNuevo.Documento.ToString();
                    pCTemporalFGNuevo.MontoRealString = ToCurrencyString(pCTemporalFGNuevo.MontoReal);
                    pCTemporalFGNuevo.IdPeriodo = proceso.IdPeriodo;
                    if (!ExisteHistorico(pCTemporalFGNuevo))
                    {
                        _context.PCTemporal.Add(pCTemporalFGNuevo);
                    }
                }


               
                if (ordenPignorada==false)
                {
                    //Se Crea Registro de Comision Rango Cumplimiento Cuota General (TipoPagoEnum.CumplimientoCuotaGeneral)
                    pCTemporalCC.Id = Guid.NewGuid().ToString();
                    pCTemporalCC.IdCliente = pCComisionesTemporal.IdCliente;
                    pCTemporalCC.Transaccion = pCComisionesTemporal.Transaccion;
                    pCTemporalCC.Documento = pCComisionesTemporal.Documento;
                    pCTemporalCC.Linea = pCComisionesTemporal.Linea;
                    pCTemporalCC.IdVendedor = pCComisionesTemporal.IdVendedor;
                    pCTemporalCC.Orden = pCComisionesTemporal.Orden;
                    pCTemporalCC.Producto = pCComisionesTemporal.Producto;
                    pCTemporalCC.MontoReal = pCComisionesTemporal.BsComision;
                    pCTemporalCC.BsComision = pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneral;
                    pCTemporalCC.PorcFlat = 0;
                    pCTemporalCC.ComisionRangoCumplimientoCuotaGeneral = pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneral;
                    pCTemporalCC.PorcRangoCumplimientoCuotaGeneral = pCComisionesTemporal.PorcRangoCumplimientoCuotaGeneral;

                    pCTemporalCC.TotalVentasMes = pCComisionesTemporal.TotalVentasMes;
                    pCTemporalCC.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMes;
                    pCTemporalCC.IdTipoPago = (int)TipoPagoEnum.CumplimientoCuotaGeneral;
                    pCTemporalCC.PeriodoDesde = periodoDesde;
                    pCTemporalCC.PeriodoHasta = periodoHasta;
                    pCTemporalCC.FechaRegistro = DateTime.UtcNow;
                    pCTemporalCC.CantidadCuotasCumplidas = 0;
                    pCTemporalCC.PorcCantidadCuotasCumplidas = 0;
                    pCTemporalCC.ComisionCantidadCuotasCumplidas = 0;
                    pCTemporalCC.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalCC.IdTipoPago);
                    pCTemporalCC.MontoString = ToCurrencyString(pCTemporalCC.BsComision);
                    pCTemporalCC.OrdenString = pCTemporalCC.Orden.ToString();
                    pCTemporalCC.DocumentoString = pCTemporalCC.Documento.ToString();
                    pCTemporalCC.MontoRealString = ToCurrencyString(pCTemporalCC.MontoReal);
                    pCTemporalCC.IdPeriodo = proceso.IdPeriodo;
                    apagar = CacularDiferenciaPagar(pCTemporalCC);
                    if (apagar.BsComision != 0)
                    {
                        _context.PCTemporal.Add(apagar);
                    }
                    //Se Crea Registro de Comision Rango Cumplimiento Cuota General Gerente(TipoPagoEnum.CumplimientoCuotaGeneralGerente)
                    pCTemporalCG.Id = Guid.NewGuid().ToString();
                    pCTemporalCG.IdCliente = pCComisionesTemporal.IdCliente;
                    pCTemporalCG.Transaccion = pCComisionesTemporal.Transaccion;
                    pCTemporalCG.Documento = pCComisionesTemporal.Documento;
                    pCTemporalCG.Linea = pCComisionesTemporal.Linea;
                    pCTemporalCG.IdVendedor = Gerente(pCComisionesTemporal.IdVendedor);
                    pCTemporalCG.Orden = pCComisionesTemporal.Orden;
                    pCTemporalCG.Producto = pCComisionesTemporal.Producto;
                    pCTemporalCG.MontoReal = pCComisionesTemporal.BsComision;
                    pCTemporalCG.BsComision = pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneralGerente;
                    pCTemporalCG.PorcFlat = 0;
                    pCTemporalCG.ComisionRangoCumplimientoCuotaGeneral = pCComisionesTemporal.ComisionRangoCumplimientoCuotaGeneralGerente;
                    pCTemporalCG.PorcRangoCumplimientoCuotaGeneral = pCComisionesTemporal.PorcRangoCumplimientoCuotaGeneralGerente;
                    pCTemporalCG.TotalVentasMes = pCComisionesTemporal.TotalVentasMesGerente;
                    pCTemporalCG.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMesGerente;
                    pCTemporalCG.IdTipoPago = (int)TipoPagoEnum.CumplimientoCuotaGeneralGerente;
                    pCTemporalCG.PeriodoDesde = periodoDesde;
                    pCTemporalCG.PeriodoHasta = periodoHasta;
                    pCTemporalCG.FechaRegistro = DateTime.UtcNow;
                    pCTemporalCG.CantidadCuotasCumplidas = 0;
                    pCTemporalCG.PorcCantidadCuotasCumplidas = 0;
                    pCTemporalCG.ComisionCantidadCuotasCumplidas = 0;
                    pCTemporalCG.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalCG.IdTipoPago);
                    pCTemporalCG.MontoString = ToCurrencyString(pCTemporalCG.BsComision);
                    pCTemporalCG.OrdenString = pCTemporalCG.Orden.ToString();
                    pCTemporalCG.DocumentoString = pCTemporalCG.Documento.ToString();
                    pCTemporalCG.MontoRealString = ToCurrencyString(pCTemporalCG.MontoReal);
                    pCTemporalCG.IdPeriodo = proceso.IdPeriodo;
                    apagar = CacularDiferenciaPagar(pCTemporalCG);
                    if (apagar.BsComision != 0)
                    {
                        _context.PCTemporal.Add(apagar);
                    }

                    //Se Crea Registro de Comision Cantidad cuotas cumplidas (TipoPagoEnum.CantidadCuotasCumplidas)
                    pCTemporalCA.Id = Guid.NewGuid().ToString();
                    pCTemporalCA.IdCliente = pCComisionesTemporal.IdCliente;
                    pCTemporalCA.Transaccion = pCComisionesTemporal.Transaccion;
                    pCTemporalCA.Documento = pCComisionesTemporal.Documento;
                    pCTemporalCA.Linea = pCComisionesTemporal.Linea;
                    pCTemporalCA.IdVendedor = pCComisionesTemporal.IdVendedor;
                    pCTemporalCA.Orden = pCComisionesTemporal.Orden;
                    pCTemporalCA.Producto = pCComisionesTemporal.Producto;
                    pCTemporalCA.MontoReal = pCComisionesTemporal.BsComision;
                    pCTemporalCA.BsComision = pCComisionesTemporal.ComisionCantidadCuotasCumplidas;
                    pCTemporalCA.PorcCantidadCuotasCumplidas = pCComisionesTemporal.PorcCantidadCuotasCumplidas;
                    pCTemporalCA.CantidadCuotasCumplidas = pCComisionesTemporal.CantidadCuotasCumplidas;
                    pCTemporalCA.ComisionCantidadCuotasCumplidas = pCComisionesTemporal.ComisionCantidadCuotasCumplidas;
                    pCTemporalCA.ComisionRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalCA.PorcRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalCA.PorcFlat = 0;
                    pCTemporalCA.TotalVentasMes = pCComisionesTemporal.TotalVentasMes;
                    pCTemporalCA.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMes;
                    pCTemporalCA.IdTipoPago = (int)TipoPagoEnum.CantidadCuotasCumplidas;
                    pCTemporalCA.PeriodoDesde = periodoDesde;
                    pCTemporalCA.PeriodoHasta = periodoHasta;
                    pCTemporalCA.FechaRegistro = DateTime.UtcNow;
                    pCTemporalCA.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalCA.IdTipoPago);
                    pCTemporalCA.MontoString = ToCurrencyString(pCTemporalCA.BsComision);
                    pCTemporalCA.OrdenString = pCTemporalCA.Orden.ToString();
                    pCTemporalCA.DocumentoString = pCTemporalCA.Documento.ToString();
                    pCTemporalCA.MontoRealString = ToCurrencyString(pCTemporalCA.MontoReal);
                    pCTemporalCA.IdPeriodo = proceso.IdPeriodo;
                    apagar = CacularDiferenciaPagar(pCTemporalCA);
                    if (apagar.BsComision != 0)
                    {
                        _context.PCTemporal.Add(apagar);
                    }

                    //Se Crea Registro de Comision Cantidad cuotas cumplidas Gerente(TipoPagoEnum.CantidadCuotasCumplidasGerente)
                    pCTemporalCB.Id = Guid.NewGuid().ToString();
                    pCTemporalCB.IdCliente = pCComisionesTemporal.IdCliente;
                    pCTemporalCB.Transaccion = pCComisionesTemporal.Transaccion;
                    pCTemporalCB.Documento = pCComisionesTemporal.Documento;
                    pCTemporalCB.Linea = pCComisionesTemporal.Linea;
                    pCTemporalCB.IdVendedor = Gerente(pCComisionesTemporal.IdVendedor);
                    pCTemporalCB.Orden = pCComisionesTemporal.Orden;
                    pCTemporalCB.Producto = pCComisionesTemporal.Producto;
                    pCTemporalCB.MontoReal = pCComisionesTemporal.BsComision;
                    pCTemporalCB.BsComision = pCComisionesTemporal.ComisionCantidadCuotasCumplidasGerente;
                    pCTemporalCB.PorcCantidadCuotasCumplidas = pCComisionesTemporal.PorcCantidadCuotasCumplidasGerente;
                    pCTemporalCB.CantidadCuotasCumplidas = pCComisionesTemporal.CantidadCuotasCumplidasGerente;
                    pCTemporalCB.ComisionCantidadCuotasCumplidas = pCComisionesTemporal.ComisionCantidadCuotasCumplidasGerente;
                    pCTemporalCB.ComisionRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalCB.PorcRangoCumplimientoCuotaGeneral = 0;
                    pCTemporalCB.PorcFlat = 0;
                    pCTemporalCB.TotalVentasMes = pCComisionesTemporal.TotalVentasMesGerente;
                    pCTemporalCB.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMesGerente;
                    pCTemporalCB.IdTipoPago = (int)TipoPagoEnum.CantidadCuotasCumplidasGerente;
                    pCTemporalCB.PeriodoDesde = periodoDesde;
                    pCTemporalCB.PeriodoHasta = periodoHasta;
                    pCTemporalCB.FechaRegistro = DateTime.UtcNow;
                    pCTemporalCB.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporalCA.IdTipoPago);
                    pCTemporalCB.MontoString = ToCurrencyString(pCTemporalCB.BsComision);
                    pCTemporalCB.OrdenString = pCTemporalCB.Orden.ToString();
                    pCTemporalCB.DocumentoString = pCTemporalCB.Documento.ToString();
                    pCTemporalCB.MontoRealString = ToCurrencyString(pCTemporalCB.MontoReal);
                    pCTemporalCB.IdPeriodo = proceso.IdPeriodo;
                    apagar = CacularDiferenciaPagar(pCTemporalCB);
                    if (apagar.BsComision != 0)
                    {
                        _context.PCTemporal.Add(apagar);
                    }

                }
               



            }
            else
            {
                //Se Crea Registro de Comision  no calculada
                pCTemporal.Id = Guid.NewGuid().ToString();
                pCTemporal.IdCliente = pCComisionesTemporal.IdCliente;
                pCTemporal.Transaccion = pCComisionesTemporal.Transaccion;
                pCTemporal.Documento = pCComisionesTemporal.Documento;
                pCTemporal.Linea = pCComisionesTemporal.Linea;
                pCTemporal.IdVendedor = pCComisionesTemporal.IdVendedor;
                pCTemporal.Orden = pCComisionesTemporal.Orden;
                pCTemporal.Producto = pCComisionesTemporal.Producto;
                pCTemporal.MontoReal = pCComisionesTemporal.MontoReal;
                pCTemporal.BsComision = pCComisionesTemporal.BsComision;
                pCTemporal.PorcFlat = 0;
                pCTemporal.TotalVentasMes = pCComisionesTemporal.TotalVentasMes;
                pCTemporal.TotalCuotaMes = pCComisionesTemporal.TotalCuotaMes;
                pCTemporal.IdTipoPago = pCComisionesTemporal.IdTipoPago;
                pCTemporal.CantidadCuotasCumplidas = 0;
                pCTemporal.PeriodoDesde = periodoDesde;
                pCTemporal.PeriodoHasta = periodoHasta;

                pCTemporal.ComisionRangoCumplimientoCuotaGeneral = 0;
                pCTemporal.PorcRangoCumplimientoCuotaGeneral = 0;
                pCTemporal.FechaRegistro = DateTime.UtcNow;
                pCTemporal.DescripcionTipoPago = GetDesripcionTipoPago(pCTemporal.IdTipoPago);
                pCTemporal.MontoString = ToCurrencyString(pCTemporal.BsComision);
                pCTemporal.OrdenString = pCTemporal.Orden.ToString();
                pCTemporal.DocumentoString = pCTemporal.Documento.ToString();
                pCTemporal.MontoRealString = ToCurrencyString(pCTemporal.MontoReal);
                pCTemporal.IdPeriodo = proceso.IdPeriodo;
                _context.PCTemporal.Add(pCTemporal);
            }




            _context.SaveChanges();










        }

        public bool ExisteHistorico(PCTemporal pCTemporal)
        {
            bool existe = false;

            PCHistorico pCHistorico = new PCHistorico();
            pCHistorico = _context.PCHistorico.Where(h => h.Transaccion == pCTemporal.Transaccion &&
                                                      h.Documento == pCTemporal.Documento && h.Linea == pCTemporal.Linea &&
                                                      h.Producto == pCTemporal.Producto && h.IdTipoPago == pCTemporal.IdTipoPago)
                                               .FirstOrDefault();
            if (pCHistorico == null)
            {
                existe = false;
            }
            else
            {
                existe = true;
            }

            return existe;

        }
        public PCTemporal CacularDiferenciaPagar(PCTemporal pCTemporal)
        {
            decimal pagadoHistorico = 0;
            decimal diferenciaComision = 0;
            decimal ultimaCuota = 0;
            int cantidaCuotaCumplida = 0;
            PCSysFile pCSysfile = new PCSysFile();
            pCSysfile = _context.PCSysfile.FirstOrDefault();
            if (!ExisteHistorico(pCTemporal))
            {
                return pCTemporal;
            }
            else
            {

                pagadoHistorico = TotalPagadoDocumento(pCTemporal);
                var comision = pCTemporal.BsComision;
                diferenciaComision = comision - pagadoHistorico;


                bool EnTolerancia = IsWithin(diferenciaComision, pCSysfile.ToleranciaDesde, pCSysfile.ToleranciaHasta);
                if (EnTolerancia)
                {
                    diferenciaComision = 0;
                }

                if (diferenciaComision > 0)
                {
                    pCTemporal.BsComision = diferenciaComision;
                }
                else if (diferenciaComision == 0)
                {
                    pCTemporal.BsComision = 0;

                }
                else
                {

                    pCTemporal.BsComision = diferenciaComision;
                    if (pCTemporal.IdTipoPago == (int)TipoPagoEnum.CumplimientoCuotaGeneral)
                    {
                        ultimaCuota = UltimaCuotaDelDocumento(pCTemporal);
                        if (pCTemporal.TotalCuotaMes != ultimaCuota)
                        {
                            pCTemporal.BsComision = 0;
                        }
                    }
                    if (pCTemporal.IdTipoPago == (int)TipoPagoEnum.CantidadCuotasCumplidas)
                    {
                        cantidaCuotaCumplida = UltimaCAntidadCuotaCumplida(pCTemporal);
                        if (pCTemporal.CantidadCuotasCumplidas != cantidaCuotaCumplida)
                        {
                            pCTemporal.BsComision = diferenciaComision;
                        }
                    }

                }

                return pCTemporal;
            }


        }
        public decimal UltimaCuotaDelDocumento(PCTemporal pCTemporal)
        {
            decimal cuota = 0;

            PCHistorico pCHistorico = new PCHistorico();
            pCHistorico = _context.PCHistorico.Where(h => h.Transaccion == pCTemporal.Transaccion &&
                                                      h.Documento == pCTemporal.Documento && h.Linea == pCTemporal.Linea &&
                                                      h.Producto == pCTemporal.Producto && h.IdTipoPago == pCTemporal.IdTipoPago)
                                                      .OrderBy(h => h.FechaRegistro)
                                                      .FirstOrDefault();
            if (pCHistorico == null)
            {
                cuota = 0;
            }
            else
            {
                cuota = pCHistorico.TotalCuotaMes;
            }

            return cuota;

        }


        public bool PagarCantidadCuotasCumplidas(PCTemporal pCTemporal)
        {
            bool pagar = false;




            return pagar;

        }

        public int UltimaCAntidadCuotaCumplida(PCTemporal pCTemporal)
        {
            int cantidad = 0;

            PCHistorico pCHistorico = new PCHistorico();
            pCHistorico = _context.PCHistorico.Where(h => h.Transaccion == pCTemporal.Transaccion &&
                                                      h.Documento == pCTemporal.Documento && h.Linea == pCTemporal.Linea &&
                                                      h.Producto == pCTemporal.Producto && h.IdTipoPago == pCTemporal.IdTipoPago)
                                                      .OrderBy(h => h.FechaRegistro)
                                                      .FirstOrDefault();
            if (pCHistorico == null)
            {
                cantidad = 0;
            }
            else
            {
                cantidad = pCHistorico.CantidadCuotasCumplidas;
            }

            return cantidad;

        }

        public decimal TotalPagadoDocumento(PCTemporal pCTemporal)
        {
            decimal totalPagadoDocumento = 0;


            List<PCHistorico> pCHistorico = new List<PCHistorico>();
            pCHistorico = _context.PCHistorico.Where(h => h.Transaccion == pCTemporal.Transaccion &&
                                                      h.Documento == pCTemporal.Documento && h.Linea == pCTemporal.Linea &&
                                                      h.Producto == pCTemporal.Producto && h.IdTipoPago == pCTemporal.IdTipoPago)
                                               .ToList();
            if (pCHistorico == null)
            {
                totalPagadoDocumento = 0;
            }
            else
            {

                foreach (var item in pCHistorico)
                {
                    totalPagadoDocumento = totalPagadoDocumento + item.BsComision;
                }
            }



            return totalPagadoDocumento;

        }
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
        public decimal PorcRangoCumplimientoCuotaGeneralGerente(decimal PorcCumplimiento)
        {

            decimal porc = 0;

            PCRangoCumplimientoCuotaGeneral pCRangoCumplimientoCuotaGeneral = new PCRangoCumplimientoCuotaGeneral();
            pCRangoCumplimientoCuotaGeneral = _context.PCRangoCumplimientoCuotaGeneral.Where(r => PorcCumplimiento >= r.Desde && PorcCumplimiento <= r.Hasta).FirstOrDefault();
            if (pCRangoCumplimientoCuotaGeneral != null)
            {
                porc = pCRangoCumplimientoCuotaGeneral.PorcentajeGerente;
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
        public decimal PorcFlatGerente(int IdSubcategoria, string gerente)
        {

            int IdTablaFlatComision = 0;
            decimal porcFlat = 0;

            //WSMY437 Tabla de Subcategoria de productos
            WSMY437 _WSMY437 = new WSMY437();

            PCFlatComisionGerente pCFlatComision = new PCFlatComisionGerente();

            _WSMY437 = _context.WSMY437.Find(IdSubcategoria);

            if (_WSMY437 != null)
            {
                IdTablaFlatComision = (int)_WSMY437.IdTablaFlatComision;

                pCFlatComision = _context.PCFlatComisionGerente.Where(f => f.IdFlatComision == IdTablaFlatComision && f.Gerente == gerente).FirstOrDefault();

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

        public string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }



        public bool IsWithin(decimal value, decimal minimum, decimal maximum)
        {
            bool result = false;
            if (value >= minimum && value <= maximum)
            {
                result = true;
            }
            return result;
        }


        public string GetDesripcionTipoPago(int idTipoPago)
        {
            string descripcion = "";
            var pCTipoPago = _context.PCTipoPago.Where(t => t.Id == idTipoPago).FirstOrDefault();
            if (pCTipoPago != null)
            {
                descripcion = pCTipoPago.Descripcion;
            }
            return descripcion;
        }
        public string ToCurrencyString(decimal d)
        {


            return String.Format("{0:0,0.0}", d);
        }


        public string Gerente(string Vendedor)
        {
            string Supervisor = "";
            PCVendedor pCVendedor = new PCVendedor();
            pCVendedor = _context.PCVendedor.Where(v => v.Codigo == Vendedor.Trim()).FirstOrDefault();
            if (pCVendedor != null)
            {
                Supervisor = pCVendedor.Supervisor;
            }

            return Supervisor;
        }

        public bool ClientePagaComisionDoble(string cliente, DateTime fechaOrden)
        {

            _context.Database.ExecuteSqlCommand("PCSpActualizaCliente @p0", cliente);
            PCSysFile pCSysfile = new PCSysFile();
            pCSysfile = _context.PCSysfile.FirstOrDefault();
            int DiasNuevo = pCSysfile.DiasClienteNuevo;
            int DiasReactivado = pCSysfile.DiasClienteReactivado;
            bool result = false;
            DateTime? FechaUltimaCompra;
            DateTime? FechaApertura;
            PCCliente pCCliente = new PCCliente();

            pCCliente = _context.PCCliente.Where(t => t.Cliente == cliente).FirstOrDefault();
            if (pCCliente != null)
            {
                FechaUltimaCompra = pCCliente.F_Ultm_Compra;
                FechaApertura = pCCliente.F_Apertura;

                TimeSpan? ts = fechaOrden - FechaApertura;
                int diasApertura = ts.Value.Days;


                if (diasApertura <= DiasNuevo)
                {
                    result = true;
                }

                ts = fechaOrden - FechaUltimaCompra;
                int diasUltimaCompra = ts.Value.Days;
                if (diasUltimaCompra >= DiasReactivado)
                {
                    result = true;
                }

            }


            return result;
        }

        public DateTime? GetfechaOrden(string orden)
        {
            _context.Database.ExecuteSqlCommand("PCAñoOrden @p0", orden);
            DateTime? result = null;
            PCAñoMesOrden pCAñoMesOrden = new PCAñoMesOrden();
            pCAñoMesOrden = _context.PCAñoMesOrden.Where(v => v.Orden == orden).FirstOrDefault();
            if (pCAñoMesOrden != null)
            {
                result = pCAñoMesOrden.FechaOrden;
            }
            return result;
        }

        public bool OrdenPignorada(string orden)
        {
            bool result = false;


            PCOrdenesPignoradas pCOrdenesPignoradas = new PCOrdenesPignoradas();
            pCOrdenesPignoradas = _context.PCOrdenesPignoradas.Where(v => v.Orden == orden).FirstOrDefault();
            if (pCOrdenesPignoradas != null)
            {
                result = true;
            }


            return result;

        }


        #endregion

    }
}
