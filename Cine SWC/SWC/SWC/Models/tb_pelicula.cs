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
    
    public partial class tb_pelicula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_pelicula()
        {
            this.tb_funcion = new HashSet<tb_funcion>();
        }
    
        public int idPelicula { get; set; }
        public string nombre { get; set; }
        public string foto { get; set; }
        public string sinopsis { get; set; }
        public Nullable<System.TimeSpan> duracion { get; set; }
        public string trailer { get; set; }
        public string pais { get; set; }
        public Nullable<int> idGenero { get; set; }
        public Nullable<int> idCensura { get; set; }
    
        public virtual tb_censura tb_censura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_funcion> tb_funcion { get; set; }
        public virtual tb_genero tb_genero { get; set; }
    }
}
