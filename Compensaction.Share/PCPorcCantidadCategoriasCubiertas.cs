using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCPorcCantidadCategoriasCubiertas
    {

        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Porcentaje { get; set; }

    }
}
