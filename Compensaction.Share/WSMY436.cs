using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public  class WSMY436
    {

        [Key]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? IdPrint { get; set; }

      
        public virtual ICollection<WSMY437> WSMY437 { get; set; }

    }
}
