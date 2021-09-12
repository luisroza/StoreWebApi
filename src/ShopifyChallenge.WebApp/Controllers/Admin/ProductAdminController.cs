using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Catalog.Application.Services;
using ShopifyChallenge.Catalog.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace ShopifyChallenge.WebApp.Controllers.Admin
{
    public class ProductAdminController : Controller
    {
        private readonly IProductService _productService;

        public ProductAdminController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("product-list")]
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAll());
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);

            //await _productService.AddProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await _productService.GetById(id));
        }

        [HttpPost]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productService.GetById(id);
            productViewModel.InventoryQuantity = product.InventoryQuantity;

            ModelState.Remove("InventoryQuantity");
            if (!ModelState.IsValid) return View(productViewModel);

            //await _productService.UpdateProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-inventory")]
        public async Task<IActionResult> UpdateInventory(Guid id)
        {
            return View("Inventory", await _productService.GetById(id));
        }

        [HttpPost]
        [Route("products-update-inventory")]
        public async Task<IActionResult> UpdateInventory(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productService.AddInventory(id, quantity);
            }
            else
            {
                await _productService.DecreaseInventory(id, quantity);
            }

            return View("Index", await _productService.GetAll());
        }
    }
}
