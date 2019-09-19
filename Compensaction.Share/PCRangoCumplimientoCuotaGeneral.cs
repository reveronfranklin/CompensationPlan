using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCRangoCumplimientoCuotaGeneral
    {

        public int Id { get; set; }

        [Required]
        public decimal Desde { get; set; }

        [Required]
        public decimal Hasta { get; set; }

        [Required]
        public decimal Porcentaje { get; set; }

        [Required]
        public decimal PorcentajeGerente { get; set; }
    }
}
