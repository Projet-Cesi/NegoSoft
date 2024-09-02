using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Services.CustomerService;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Models.Extensions;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: Customers/Details/{CusId}
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerService.GetCustomerByIdAsync(id.Value);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerViewModel customer)
    {
        if (customer == null)
        {
               return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _customerService.SaveCustomer(customer);
            return RedirectToAction("Create","Address");
        }
        return View(customer);
    }

    // POST: Customers/SetDefaultAdress/{CusId}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetDefaultAdress(Guid CusId, Guid AddId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(CusId);
        if (customer == null)
        {
            return NotFound();
        }
        customer.CusDefaultAddressId = AddId;
        await _customerService.UpdateCustomerAsync(customer);
        return RedirectToAction("Index", "Customer");
    }
}

