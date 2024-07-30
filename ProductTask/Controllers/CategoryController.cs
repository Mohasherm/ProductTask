using ButterflyApi.Base.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTask.Repository.Main.Category;
using ProductTask.Repository.Main.Category.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryRepository icategoryRepository;

        public CategoryController(IcategoryRepository icategoryRepository)
        {
            this.icategoryRepository = icategoryRepository;
        }



        [HttpPost]
        [Authorize]
        [Produces(typeof(bool))]
        public async Task<IActionResult> AddCategory(AddCategoryDto dto)
        {
            var res = await icategoryRepository.AddCategory(dto);
            return res.GetResult();
        }

        [HttpPut]
        [Authorize]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var res = await icategoryRepository.UpdateCategory(dto);
            return res.GetResult();
        }


        [HttpGet]
        [Produces(typeof(List<GetCategoryDto>))]
        public async Task<IActionResult> GetAllCategory()
        {
            var res = await icategoryRepository.GetAllCategory();
            return res.GetResult();
        }

        [HttpGet]
        [Produces(typeof(GetCategoryDto))]
        public async Task<IActionResult> GetCategoryById([FromQuery] Guid Id)
        {
            var res = await icategoryRepository.GetCategoryById(Id);
            return res.GetResult();
        }

        [HttpDelete]
        [Authorize]
        [Produces(typeof(bool))]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid Id)
        {
            var res = await icategoryRepository.DeleteCategory(Id);
            return res.GetResult();
        }




    }
}
