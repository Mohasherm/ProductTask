using ButterflyApi.Base.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTask.Repository.Main.Product;
using ProductTask.Repository.Main.Product.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpPost]
        [Authorize]
        [Produces(typeof(bool))]
        public async Task<IActionResult> AddProduct(AddProduct dto)
        {
            var res = await productRepository.AddProduct(dto);
            return res.GetResult();
        }

        [HttpPut]
        [Authorize]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateProduct(UpdateProduct dto)
        {
            var res = await productRepository.UpdateProduct(dto);
            return res.GetResult();
        }

        [HttpDelete]
        [Authorize]
        [Produces(typeof(bool))]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            var res = await productRepository.DeleteProduct(Id);
            return res.GetResult();
        }

        [HttpGet]
        [Produces(typeof(List<GetProductsDto>))]
        public async Task<IActionResult> GetAllProducts()
        {
            var res = await productRepository.GetAllProducts();
            return res.GetResult();
        }

        [HttpGet]
        [Produces(typeof(List<GetProductsDto>))]
        public async Task<IActionResult> GetAllProductsByCategoryId(Guid Id)
        {
            var res = await productRepository.GetAllProductsByCategoryId(Id);
            return res.GetResult();
        }

        [HttpGet]
        [Produces(typeof(GetProductsDto))]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            var res = await productRepository.GetProductById(Id);
            return res.GetResult();
        }

    }
}
