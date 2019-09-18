using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCTasaAñoMes
    {
        public int Id { get; set; }

        [Required]
        public int Año { get; set; }

        [Required]
        public int Mes { get; set; }

        [Required]
        public decimal Tasa { get; set; }

    }
}
