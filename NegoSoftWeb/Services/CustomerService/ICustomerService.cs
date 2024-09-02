﻿using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.ViewModels;


namespace NegoSoftWeb.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> GetCustomerAsync();
        void SaveCustomer(CustomerViewModel customer);
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task AddCustomerAsync(CustomerViewModel customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
        Task<bool> CustomerExists(Guid id);


    }
}
