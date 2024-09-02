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
        Task AddAddressAsync(AddressViewModel address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(Guid id);
        Task<bool> AddressExists(Guid id);
    }
}
