﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public class PCSysFile
    {
        public int Id { get; set; }
                
        public decimal ToleranciaDesde { get; set; }

        public decimal ToleranciaHasta { get; set; }

        public decimal PorcCunplimiento { get; set; }

        public int DiasClienteNuevo { get; set; }

        public int DiasClienteReactivado { get; set; }



        public decimal UmbralOrdenesPignoradas{ get; set; }
               
//<<<<<<< HEAD
  
//=======
//>>>>>>> 17fa1a4fda41ff04cb0d68a5d6966eee2cc13074

        public int DiasPagoDoble { get; set; }

    }

}
