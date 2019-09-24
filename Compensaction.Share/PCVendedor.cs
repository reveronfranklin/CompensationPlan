using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCVendedor
    {
        public int Id { get; set; }

        public string IdVendedor { get; set; }

        public string Nombre { get; set; }

        public int CodOficina { get; set; }

        public bool Activo { get; set; }

        public string Ficha { get; set; }

        public string Supervisor { get; set; }

        public bool Gerente { get; set; }

        public bool NoPagarComision { get; set; }
    }
}
