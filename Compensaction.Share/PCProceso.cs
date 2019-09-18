using System;
using System.Collections.Generic;
using System.Text;

namespace Compensaction.Share
{
    public class PCProceso
    {
        public int Id { get; set; }
        public string PeriodoDesde { get; set; }
        public string PeriodoHasta { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int TotalReg { get; set; }
        public int RegistrosProcesados { get; set; }
        public bool Iniciado { get; set; }
        public bool Culminado { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public int Accion { get; set; }
        public int RegistrosCerrados { get; set; }
        public bool PeriodoCerrado { get; set; }
        public int IdPeriodo { get; set; }


    }
}
