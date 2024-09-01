using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace NegoSoftShared.Models.Entities
{
    public class Product
    {
        // Propriétés de la classe Product
        [Key]
        [Column("pro_id")]
        public Guid ProId { get; set; }

        [Column("pro_name")]
        public string ProName { get; set; }

        [Column("pro_description")]
        public string ProDescription { get; set; }

        [ForeignKey("Supplier")]
        [Column("pro_supplier_id")]
        public Guid ProSupplierId { get; set; }

        [Column("pro_price")]
        public float ProPrice { get; set; }

        [ForeignKey("Type")]
        [Column("pro_type_id")]
        public Guid ProTypeId { get; set; }

        [Column("pro_stock")]
        public int ProStock { get; set; }

        [Column("pro_picture_name")]
        public string ProPictureName { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public Supplier Supplier { get; set; }
        public Type Type { get; set; }
        public ICollection<SupplierOrderDetails> SupplierOrderDetails { get; set; }
        public ICollection<CustomerOrderDetails> CustomerOrderDetails { get; set; }
        public AlcoholProduct AlcoholProduct { get; set; }
    }
}
