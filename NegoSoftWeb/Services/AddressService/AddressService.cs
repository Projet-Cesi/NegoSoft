﻿using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.Extensions;
using NegoSoftWeb.Models.ViewModels;

namespace NegoSoftWeb.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private const string AddressSessionKey = "Address";
        private readonly NegoSoftContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressService(NegoSoftContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Méthode pour récupérer l'adresse depuis la session
        public Task<AddressViewModel> GetAddressAsync()
        {
            var address = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<AddressViewModel>(AddressSessionKey);
            return Task.FromResult(address);
        }

        public void SaveAddress(AddressViewModel address)
        {
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson(AddressSessionKey, address);
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AddId == id);
        }

        public async Task<Address> AddAddressAsync(AddressViewModel address)
        {
            var addressExists = await AddressExists(address);

            // L'adresse existe déjà alors on la retourne
            if (addressExists != null)
            {
                return addressExists;
            }

            // L'adresse n'existe pas alors on l'ajoute
            var newAddress = new Address
            {
                AddId = Guid.NewGuid(),
                AddDeliveryStreet = address.AddDeliveryStreet,
                AddDeliveryCity = address.AddDeliveryCity,
                AddDeliveryZipCode = address.AddDeliveryZipCode,
                AddDeliveryCountry = address.AddDeliveryCountry,
                AddBillingStreet = address.AddBillingStreet,
                AddBillingCity = address.AddBillingCity,
                AddBillingZipCode = address.AddBillingZipCode,
                AddBillingCountry = address.AddBillingCountry
            };
            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
            return newAddress;
        }

        public async Task<Address> AddressExists(AddressViewModel address)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AddDeliveryStreet == address.AddDeliveryStreet &&
                                            a.AddDeliveryCity == address.AddDeliveryCity &&
                                            a.AddDeliveryZipCode == address.AddDeliveryZipCode &&
                                            a.AddDeliveryCountry == address.AddDeliveryCountry &&
                                            a.AddBillingStreet == address.AddBillingStreet &&
                                            a.AddBillingCity == address.AddBillingCity &&
                                            a.AddBillingZipCode == address.AddBillingZipCode &&
                                            a.AddBillingCountry == address.AddBillingCountry);
        }
    }
}

