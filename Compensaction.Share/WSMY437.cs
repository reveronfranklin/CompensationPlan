using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compensaction.Share
{
    public  class WSMY437
    {

        [Key]
        public int IdSubCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdProductoCuota { get; set; }
        public string ValidaMc { get; set; }
        public int? DiasCorreo { get; set; }
        public decimal? PorcCypj { get; set; }
        public decimal? PorcCypjminimo { get; set; }
        public string TipoMaterialSap { get; set; }
        public string GrupoArticulo { get; set; }
        public string Sector { get; set; }
        public string Imputacion { get; set; }
        public string TipoPosicion { get; set; }
        public string GrupoMaterial { get; set; }
        public string GrupoMaterial1 { get; set; }
        public string GrupoMaterial2 { get; set; }
        public string GrupoMaterial3 { get; set; }
        public string CentroDeBeneficio { get; set; }
        public string IndicadorAbc { get; set; }
        public string PlanificacionNecesidades { get; set; }
        public string CategoriaValoracion { get; set; }
        public string TipoValoracion { get; set; }
        public string IndicadorControldePrecios { get; set; }
        public decimal? PorcCypjgob { get; set; }
        public decimal? PorcCypjgobMinimo { get; set; }
        public int? IdTablaFlatComision { get; set; }
        public int IdCuotaComision { get; set; }

        public virtual WSMY436 IdCategoriaNavigation { get; set; }
       
    }
}
