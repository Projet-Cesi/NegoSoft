using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.Entities;
using System;
using System.Collections.Generic;

namespace NegoSoftWeb.Models.ViewModels
{
    public class ProductSearchViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Guid? SelectedTypeId { get; set; }
        public Guid? SelectedSupplierId { get; set; }
        public Guid? SelectedAlcoholProductId { get; set; }
        public SortOrder SelectedSortOrder { get; set; } 
        public IEnumerable<NegoSoftShared.Models.Entities.Type> ProductTypes { get; set; }
        public IEnumerable<AlcoholProduct> AlcoholProducts { get; set; }
        public IEnumerable<Supplier> ProductSuppliers { get; set; }

    }
}
