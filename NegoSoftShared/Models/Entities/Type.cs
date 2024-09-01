using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class Type
    {
        // Propriétés de la classe Type
        [Key]
        [Column("typ_id")]
        public Guid TypId { get; set; }

        [Required]
        [Column("typ_libelle")]
        public string TypLibelle { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public ICollection<Product> Products { get; set; }
    }

}
