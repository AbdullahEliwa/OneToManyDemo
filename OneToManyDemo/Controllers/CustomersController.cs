using Dto.Dtos;
using Microsoft.AspNetCore.Mvc;
using OneToManyDemo.Controllers.Base;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneToManyDemo.Controllers
{
    public class CustomersController : BaseController
    {
        #region CTOR, Fields.
        private const string CustomerForm = "CustomerForm";

        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Read Operations.
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.Get(id);
            if (customer is null)
                return NotFound();

            return View(customer);
        }
        #endregion

        #region Create Operations.
        public IActionResult Create()
        {
            return View(CustomerForm, new CustomerDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            var result = await Do(async () => await _customerService.Create(customerDto));
            if (result.Faild)
                return View(CustomerForm, customerDto);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update Operations.
        public async Task<IActionResult> Update(int id)
        {
            var customer = await _customerService.Get(id);
            if (customer is null)
                return NotFound();
            return View(CustomerForm, customer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerDto customerDto)
        {
            var result = await Do(async () => await _customerService.Update(customerDto));
            if (result.Faild)
                return View(CustomerForm, customerDto);
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete Operations.
        //TODO: Enhance this using json.
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Do(async () => await _customerService.Delete(id));
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
