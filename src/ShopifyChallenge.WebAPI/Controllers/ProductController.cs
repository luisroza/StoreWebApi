using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Catalog.Application.Services;
using Store.Catalog.Application.ViewModels;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Messages.Notifications;
using Store.WebAPI.Extensions;
using Store.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Store.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(INotificationHandler<DomainNotification> notification,
                                  IProductService productService,
                                  IMediatorHandler mediatorHandler,
                                  IUser user) : base(notification, mediatorHandler, user)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return await _productService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var product = await _productService.GetById(id);

            if (product == null) return NotFound();

            return product;
        }

        [ClaimsAuthorize("Product", "Add")]
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _productService.Add(productViewModel);

            return CustomResponse(productViewModel);
        }

        [ClaimsAuthorize("Product", "Edit")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                NotifyError("Product", "IDs do not match");
                return CustomResponse();
            }

            var productUpdate= await _productService.GetById(id);

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            productUpdate.ProductImages = productViewModel.ProductImages;
            productUpdate.Name = productViewModel.Name;
            productUpdate.Description = productViewModel.Description;
            productUpdate.Price = productViewModel.Price;
            productUpdate.Active = productViewModel.Active;
            productUpdate.InventoryQuantity = productViewModel.InventoryQuantity;

            await _productService.Update(productUpdate);

            return CustomResponse(productViewModel);
        }

        [ClaimsAuthorize("Product", "AddImage")]
        [HttpPost("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> AddImage(Guid id, IEnumerable<ProductImageViewModel> imageViewModelList)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            foreach (var image in imageViewModelList)
            {
                var imgName = Guid.NewGuid() + "_" + image.Name;
                if (!UploadFile(image.FileBase, imgName))
                {
                    return CustomResponse(image);
                }

                image.ProductId = id;
                await _productService.AddImage(id, image);
            }

            return CustomResponse(imageViewModelList);
        }

        [ClaimsAuthorize("Product", "RemoveImage")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductImageViewModel>> RemoveImage(Guid id)
        {
            var productImageViewModel = await _productService.GetImageById(id);
            if (productImageViewModel == null) return NotFound();

            await _productService.RemoveImage(id);

            return CustomResponse(productImageViewModel);
        }

        private bool UploadFile(string file, string imgName)
        {
            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Product", "The image needs a name");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(file);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imgName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Product", "A file with this name already exists");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
    }
}
