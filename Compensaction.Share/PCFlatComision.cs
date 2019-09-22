using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCFlatComision
    {

        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal Porcentaje { get; set; }



    }
}
