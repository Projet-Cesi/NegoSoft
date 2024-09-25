using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NegoSoftShared.Models.Entities
{
    public class AlcoholProduct
    {
        // Propriétés de la classe AlcoholProduct
        [Key]
        [Column("ap_id")]
        public Guid ApId { get; set; }

        [Required]
        [ForeignKey("Product")]
        [Column("pro_type_id")]
        public Guid ProTypeId { get; set; }

        [Required]
        [Column("ap_alcohol_volume")]
        public float ApAlcoholVolume { get; set; }

        [Required]
        [Column("ap_year")]
        public int ApYear { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public Product Product { get; set; }
    }

}
