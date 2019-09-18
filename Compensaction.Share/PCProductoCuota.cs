using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCProductoCuota
    {

        public int Id { get; set; }

        [Required]
        public string  Descripcion { get; set; }
    }
}
