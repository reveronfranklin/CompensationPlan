using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCOrdenesPignoradas
    {

        public int Id { get; set; }

        public string  Orden { get; set; }

        public string Cotizacion { get; set; }

        public string IdVendedor { get; set; }

        public string NombreVendedor { get; set; }

        public string IdCliente { get; set; }

        public string NombreCliente { get; set; }
    }
}
