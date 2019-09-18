﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCCuotaVentas
    {
        public int Id { get; set; }

        public int Año { get; set; }

        public int Mes { get; set; }

        public string Vendedor { get; set; }

        public int IdProductoCuota { get; set; }

        public decimal Cuota { get; set; }

        public decimal Venta { get; set; }

        public decimal PorcCumplimiento { get; set; }

        public decimal CuotaUsd { get; set; }

        public decimal VentaUsd { get; set; }

        public decimal TasaUsd { get; set; }

        public string NombreVendedor { get; set; }

        public string DescripcionCuota { get; set; }

        public string CuotaString { get; set; }

        public string VentaString { get; set; }

        public string CuotaUsdString { get; set; }

        public string VentaUsdString { get; set; }

        public string TasaUsdString { get; set; }



    }
}
