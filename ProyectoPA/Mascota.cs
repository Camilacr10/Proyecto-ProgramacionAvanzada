//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPA
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mascota
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mascota()
        {
            this.Adopcions = new HashSet<Adopcion>();
            this.Requisitoes = new HashSet<Requisito>();
        }
    
        public int id_mascota { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public string especie { get; set; }
        public string raza { get; set; }
        public Nullable<decimal> peso { get; set; }
        public string sexo { get; set; }
        public string castrado { get; set; }
        public string vacunado { get; set; }
        public string desparasitado { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fecha_rescate { get; set; }
        public string ruta_imagen { get; set; }
        public string disponibilidad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adopcion> Adopcions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requisito> Requisitoes { get; set; }
    }
}
