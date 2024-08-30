using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models
{
    internal class Type
    {
        public Guid TypeId { get; set; }
        public string? Name { get; set; }
        public Guid ProductId  { get; set; }
        public virtual Product Product { get; set; }
    }
}
