using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Catalog.Application.Services;
using StoreApi.Core.Communication.Mediator;
using StoreApi.Core.Communication.Messages.Notifications;
using System;
using System.Threading.Tasks;
using StoreApi.WebApi.ViewModels;

namespace StoreApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisplayController : BaseController
    {
        private readonly IProductService _productService;

        public DisplayController(INotificationHandler<DomainNotification> notification,
                                IProductService productService,
                                IMediatorHandler mediatorHandler,
                                IUser user) : base(notification, mediatorHandler, user)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDisplay()
        {
            return CustomResponse(await _productService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return CustomResponse(await _productService.GetById(id));
        }
    }
}
