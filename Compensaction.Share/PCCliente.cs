using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCCliente
    {
        public int Id { get; set; }
        public string  Cliente { get; set; }
        public string Nombre { get; set; }
        public DateTime? F_Apertura { get; set; }
        public DateTime? F_Ultm_Compra { get; set; }
        public DateTime? FechaReactivado { get; set; }
        
    }
}
