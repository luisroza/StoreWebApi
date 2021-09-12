using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Catalog.Application.Services;
using System;
using System.Threading.Tasks;

namespace ShopifyChallenge.WebApp.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IProductService _productService;

        public DisplayController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        [Route("display")]
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAll());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _productService.GetById(id));
        }
    }
}
