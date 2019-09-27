using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public  class WSMY693
    {


        [Key]
        public long Recnum { get; set; }
        public string Ficha { get; set; }
        public int IdSubCategoria { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime FechaActualiza { get; set; }





    }
}
