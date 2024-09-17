namespace NegoSoftWeb.Services.PaymentsService
{
    public interface IPaymentsService
    {
        Task<string> CreateCheckoutSessionAsync();
        Task Success();
    }
}
