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
    
    public partial class tb_tamaño
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_tamaño()
        {
            this.tb_DetalleVentaProducto = new HashSet<tb_DetalleVentaProducto>();
        }
    
        public int idTamaño { get; set; }
        public string desTamaño { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_DetalleVentaProducto> tb_DetalleVentaProducto { get; set; }
    }
}
