using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCAñoMesOrden
    {

        public int Id { get; set; }

        public string Orden { get; set; }

        public string Cotizacion { get; set; }

        public int Año { get; set; }

        public int Mes { get; set; }

        public DateTime FechaOrden { get; set; }

        public string IdVendedor { get; set; }

        public string NombreVendedor { get; set; }

        public string IdCliente { get; set; }

        public string NombreCliente { get; set; }

        public string Producto { get; set; }

    }
}
