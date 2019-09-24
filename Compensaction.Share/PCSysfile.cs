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
    }
}
