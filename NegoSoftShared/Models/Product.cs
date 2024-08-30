using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NegoSoftShared;

namespace NegoSoftShared.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Supplier { get; set; }
        public int Stock {  get; set; }
        public string PictureName { get; set; }
        public int Year { get; set; }
        public float Alcohol { get; set; } 

        //public virtual ICollection<Type>? Types { get; set; }

        //public virtual ICollection<Supplier>? Suppliers { get; set; }

        public Product() { }
    }
}
