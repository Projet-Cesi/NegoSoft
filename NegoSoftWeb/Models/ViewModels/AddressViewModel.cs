namespace NegoSoftWeb.Models.ViewModels
{
    public class AddressViewModel
    {
        public Guid AddId { get; set; }
        public string AddDeliveryStreet { get; set; }
        public string AddDeliveryCity { get; set; }
        public string AddDeliveryZipCode { get; set; }
        public string AddDeliveryCountry { get; set; }
        public string AddBillingStreet { get; set; }
        public string AddBillingCity { get; set; }
        public string AddBillingZipCode { get; set; }
        public string AddBillingCountry { get; set; }
    }
}

