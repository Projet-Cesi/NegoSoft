using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class CustomerOrderDetails
    {
        // Propriétés de la classe CustomerOrderDetails
        [Key]
        [Column("cod_id")]
        public Guid CodId { get; set; }

        [Column("cod_quantity")]
        public int CodQuantity { get; set; }

        [ForeignKey("CustomerOrder")]
        [Column("cod_order_id")]
        public Guid CodOrderId { get; set; }

        [ForeignKey("Product")]
        [Column("cod_product_id")]
        public Guid CodProductId { get; set; }

        [Column("cod_price")]
        public float CodPrice { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public CustomerOrder CustomerOrder { get; set; }
        public Product Product { get; set; }
    }

}
