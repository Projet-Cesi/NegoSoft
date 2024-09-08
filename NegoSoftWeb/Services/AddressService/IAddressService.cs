using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.ViewModels;
using System.Net;

namespace NegoSoftWeb.Services.AddressService
{
    public interface IAddressService
    {
        Task<AddressViewModel> GetAddressAsync();
        void SaveAddress(AddressViewModel address);
        Task<Address> GetAddressByIdAsync(Guid id);
        Task<Address> AddAddressAsync(AddressViewModel address);
        Task<bool> AddressExists(Guid id);
    }
}
