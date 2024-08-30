using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models
{
    internal class Supplier
    {
        public Guid SupplierId { get; set; }
        public string? Name { get; set; }

        //public string? Product {  get; set; }
        public string? Location { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
