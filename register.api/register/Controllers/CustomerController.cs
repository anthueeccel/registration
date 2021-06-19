using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using register.app.Interfaces;
using register.app.ViewModels;
using register.domain.Entities;
using register.domain.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace register.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerAppService _appService;

        public CustomerController(ICustomerAppService appService, INotificationHandler<DomainNotification> notificationHandler)
            : base(notificationHandler)
        {
            _appService = appService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerViewModel customerViewModel)
        {
            await _appService.AddAsync(customerViewModel);

            return CustomResponse();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerViewModel customerViewModel)
        {
            await _appService.UpdateAsync(customerViewModel);

            return CustomResponse();
        }
                
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _appService.GetAll();
        }

        [HttpGet("{id}")]
        public Customer GetById(Guid id)
        {
            return _appService.GetById(id);
        }

        [HttpDelete("{id}")]
        public async Task Remove(Guid id)
        {
            await _appService.RemoveAsync(id);
        }
    }
}
