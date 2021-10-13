﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Catalog.Application.Services;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Messages.Notifications;
using System;
using System.Threading.Tasks;
using Store.WebAPI.ViewModels;

namespace Store.WebAPI.Controllers
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
