using System.ComponentModel.DataAnnotations;

namespace Compensaction.Share
{
    public class PCTipoPago
    {
        public int Id { get; set; }

        [Required]
        public string TipoPago { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public bool FlagCalcular { get; set; }

    }

    public enum TipoPagoEnum
    {
      
        PagoManual = 1,
        ReversoChequeDevuelto = 2,
        ComisionFlat = 3,
        CumplimientoCuotaGeneral = 4,
        CantidadCuotasCumplidas = 5,
        RecibosAnticipos = 6,
        ComisionFlatGerente=7,
        CumplimientoCuotaGeneralGerente = 8,
        CantidadCuotasCumplidasGerente = 9,
    }
}
