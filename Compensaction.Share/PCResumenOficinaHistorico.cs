using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCResumenOficinaHistorico
    {
        public int Id { get; set; }


        public int CodigoOficina { get; set; }

        public string NombreOficina { get; set; }

        public decimal Monto { get; set; }

        public string MontoString { get; set; }

    }
}
