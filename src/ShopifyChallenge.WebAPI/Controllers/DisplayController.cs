using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Catalog.Application.Services;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using System;
using System.Threading.Tasks;
using ShopifyChallenge.WebAPI.ViewModels;

namespace ShopifyChallenge.WebAPI.Controllers
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
