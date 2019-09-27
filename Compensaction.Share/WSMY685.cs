using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public  class WSMY685
    {
        [Key]
        public long IdPago { get; set; }
        public string Transaccion { get; set; }
  
        public long Orden { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string Producto { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoGte { get; set; }
        public decimal MontoGteProducto { get; set; }
        public long FichaGteProducto { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Observaciones { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime FechaActualiza { get; set; }
        public bool FlagPagado { get; set; }
        public DateTime? FechaPagado { get; set; }
        public decimal? Rmonto { get; set; }
        public decimal? RmontoGte { get; set; }
        public decimal? RmontoGteProducto { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string OrdenString { get; set; }


    
   

}
}
