using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCCuotaVentasGerente
    {
        public int Id { get; set; }

        public int Año { get; set; }

        public int Mes { get; set; }

        public string Gerente { get; set; }

        public int IdProductoCuota { get; set; }

        public decimal Cuota { get; set; }

        public decimal Venta { get; set; }

        public decimal PorcCumplimiento { get; set; }

        public string CuotaString { get; set; }
                      

        public string VentaString { get; set; }

        public string DescripcionCuota { get; set; }






    }
}
