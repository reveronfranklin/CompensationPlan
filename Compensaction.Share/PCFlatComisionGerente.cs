using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCFlatComisionGerente
    {

        public int Id { get; set; }

        [Required]
        public int IdFlatComision { get; set; }

        [Required]
        public string Gerente { get; set; }

        [Required]
        public string DescripcionCategoria { get; set; }

        [Required]
        public decimal Porcentaje { get; set; }


        [Required]
        public string NombreGerente { get; set; }
    }
}
