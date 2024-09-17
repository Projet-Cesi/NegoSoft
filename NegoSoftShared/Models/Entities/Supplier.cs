using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class Supplier
    {
        // Propriétés de la classe Supplier
        [Key]
        [Column("sup_id")]
        public Guid SupId { get; set; }

        [Required]
        [Column("sup_name")]
        public string SupName { get; set; }

        [Required]
        [ForeignKey("Address")]
        [Column("sup_default_address_id")]
        public Guid SupDefaultAddressId { get; set; }

        [Column("sup_email")]
        public string? SupEmail { get; set; }

        [Required]
        [Column("sup_phone")]
        public string SupPhone { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public Address DefaultAddress { get; set; }
        public ICollection<SupplierOrder> SupplierOrders { get; set; }
        public ICollection<Product> Products { get; set; }
    }


}
