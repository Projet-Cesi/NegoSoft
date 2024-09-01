namespace NegoSoftWeb.Models.Entities
{
    public class CartItem
    {
        public Guid ProId { get; set; }
        public string ProName { get; set; }
        public float ProPrice { get; set; }
        public int ProQuantity { get; set; }
    }
}
