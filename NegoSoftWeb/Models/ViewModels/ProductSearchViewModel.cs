using NegoSoftShared.Models.Entities;

namespace NegoSoftWeb.Models.ViewModels
{
    public class ProductSearchViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Guid? SelectedTypeId { get; set; }
        public IEnumerable<NegoSoftShared.Models.Entities.Type> ProductTypes { get; set; }
    }
}
