using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCOrdenesPignoradas
    {

        public int Id { get; set; }

        [StringLength(10)]
        public string  Orden { get; set; }
        [StringLength(20)]
        public string Cotizacion { get; set; }

        [StringLength(4)]
        public string IdVendedor { get; set; }

        public string NombreVendedor { get; set; }

        public string IdCliente { get; set; }

        public string NombreCliente { get; set; }
    }
}
