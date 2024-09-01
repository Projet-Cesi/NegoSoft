namespace NegoSoftWeb.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProId { get; set; }
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public float ProPrice { get; set; }
        public int ProStock { get; set; }
        public string ProPictureName { get; set; }
        public Guid ProTypeId { get; set; }
        public Guid ProSupplierId { get; set; }
        public string ProType { get; set; }
        public string ProSupplier { get; set; }
        
        public float ProBoxPrice { get; set; }

        public IFormFile ProImageFile { get; set; }
    }
}
