using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class WSMY686
    {
        public int ID { get; set; }
        [Required]
        public System.DateTime Desde { get; set; }
        [Required]
        public System.DateTime Hasta { get; set; }
        public bool Activo { get; set; }
        [Required]
        public string DescPeriodo { get; set; }
        public string UsuarioActualiza { get; set; }
        public System.DateTime FechaActualiza { get; set; }

        public string FechaDescripcion { get { return String.Format("{0}/{1}/{2}-{3}/{4}/{5}-{6}", Desde.Day.ToString(),Desde.Month.ToString(), Desde.Year.ToString(), Hasta.Day.ToString(), Hasta.Month.ToString(), Hasta.Year.ToString(), DescPeriodo); } }

    }
}
