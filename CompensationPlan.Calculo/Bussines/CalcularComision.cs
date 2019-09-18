using Compensaction.Share;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    Porcentaje = pCPorcCantidadCategoriasCubiertas.Porcentaje;
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
                pCTipoPago = _context.PCTipoPago.Where(t => t.Id == pCComisionesTemporal.IdTipoPago).FirstOrDefault();
                if (pCTipoPago.FlagCalcular)
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
                        if (item.Transaccion=="PM")
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
            if (pagoManual!=null)
            {
                pagoManual.FlagPagado = true;
                pagoManual.FechaPagado = DateTime.UtcNow;
                _context.Entry(pagoManual).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public void CreaDetalleTemporal(PCComisionesTemporal pCComisionesTemporal)
        {
            PCTemporal pCTemporalPM = new PCTemporal();
            PCTemporal pCTemporalCF = new PCTemporal();
            PCTemporal pCTemporalCC = new PCTemporal();
            PCTemporal pCTemporalCA = new PCTemporal();
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
                pCTemporalCF.IdPeriodo=proceso.IdPeriodo;
                if (!ExisteHistorico(pCTemporalCF))
                {
                    _context.PCTemporal.Add(pCTemporalCF);
                }


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
            PCSysfile pCSysfile = new PCSysfile();
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


                bool EnTolerancia = IsWithin(diferenciaComision, pCSysfile .ToleranciaDesde, pCSysfile.ToleranciaHasta);
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
            if (pCTipoPago!=null)
            {
                descripcion = pCTipoPago.Descripcion;
            }
            return descripcion;
        }
        public string ToCurrencyString(decimal d)
        {
            
           
            return String.Format("{0:0,0.0}", d);
        }
        #endregion

    }
}
