using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.Entities
{
    public class CustomerOrder
    {
        // Propriétés de la classe CustomerOrder
        [Key]
        [Column("co_id")]
        public Guid CoId { get; set; }

        [Required]
        [Column("co_date")]
        public DateTime CoDate { get; set; } = DateTime.Now;

        [Required]
        [Column("co_state")]
        public string CoState { get; set; } = "En attente";

        [Required]
        [ForeignKey("Customer")]
        [Column("co_customer_id")]
        public Guid CoCustomerId { get; set; }

        [Required]
        [ForeignKey("Address")]
        [Column("co_address_id")]
        public Guid CoAddressId { get; set; }

        [Required]
        [Column("co_total")]
        public float CoTotal { get; set; }
         
        // Propriétés de navigation de la classe permettant de naviguer entre les différentes classes (voir cours sur les relations entre les classes)
        public Customer Customer { get; set; }

        public Address Address { get; set; }
        public ICollection<CustomerOrderDetails> CustomerOrderDetails { get; set; }
    }

}
