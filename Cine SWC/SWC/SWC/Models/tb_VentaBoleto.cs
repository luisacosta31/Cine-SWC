//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_VentaBoleto
    {
        public int idVentaB { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idFuncion { get; set; }
        public Nullable<int> idEmpleado { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<decimal> total { get; set; }
    
        public virtual tb_empleado tb_empleado { get; set; }
        public virtual tb_funcion tb_funcion { get; set; }
    }
}
