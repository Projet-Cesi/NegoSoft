using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class SupplierOrderDetails
    {
        // Propriétés de la classe SupplierOrderDetails
        [Key]
        [Column("sod_id")]
        public Guid SodId { get; set; }

        [Column("sod_quantity")]
        public int SodQuantity { get; set; }

        [ForeignKey("SupplierOrder")]
        [Column("sod_order_id")]
        public Guid SodOrderId { get; set; }

        [ForeignKey("Product")]
        [Column("sod_product_id")]
        public Guid SodProductId { get; set; }

        [Column("sod_price")]
        public float SodPrice { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public SupplierOrder SupplierOrder { get; set; }
        public Product Product { get; set; }
    }

}
