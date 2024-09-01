using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class Address
    {
        // Propriétés de la classe Address
        [Key]
        [Column("add_id")]
        public Guid AddId { get; set; }

        [Column("add_delivery_street")]
        public string AddDeliveryStreet { get; set; }

        [Column("add_delivery_city")]
        public string AddDeliveryCity { get; set; }

        [Column("add_delivery_zip_code")]
        public string AddDeliveryZipCode { get; set; }

        [Column("add_delivery_country")]
        public string AddDeliveryCountry { get; set; }

        [Column("add_billing_street")]
        public string AddBillingStreet { get; set; }

        [Column("add_billing_city")]

        public string AddBillingCity { get; set; }

        [Column("add_billing_zip_code")]

        public string AddBillingZipCode { get; set; }

        [Column("add_billing_country")]

        public string AddBillingCountry { get; set; }

        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
        public ICollection<SupplierOrder> SupplierOrders { get; set; }
    }

}
