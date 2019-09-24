﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCTemporal
    {

        public string Id { get; set; }

        public string IdCliente { get; set; }

        public string Transaccion { get; set; }

        public decimal Documento { get; set; }

        public short Linea { get; set; }

        public string IdVendedor { get; set; }

        public long Orden { get; set; }

        public string Producto { get; set; }

        public decimal MontoReal { get; set; }

        public decimal BsComision { get; set; }

        public decimal PorcFlat { get; set; }

        public decimal ComisionRangoCumplimientoCuotaGeneral { get; set; }

        public decimal PorcRangoCumplimientoCuotaGeneral { get; set; }

        public decimal PorcCantidadCuotasCumplidas { get; set; }

        public decimal ComisionCantidadCuotasCumplidas { get; set; }

        public int CantidadCuotasCumplidas { get; set; }

        public decimal TotalVentasMes { get; set; }

        public decimal TotalCuotaMes { get; set; }

        public int IdTipoPago { get; set; }

        public string PeriodoDesde { get; set; }

        public string PeriodoHasta { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string DescripcionTipoPago { get; set; }

        public string MontoString { get; set; }

        public string OrdenString { get; set; }

        public string DocumentoString { get; set; }

        public string MontoRealString { get; set; }

        public int IdPeriodo { get; set; }

      
    }
}
