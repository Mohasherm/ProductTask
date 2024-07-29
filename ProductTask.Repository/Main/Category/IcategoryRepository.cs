using ProductTask.Base.OperationResult;
using ProductTask.Repository.Main.Category.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.Repository.Main.Category
{
    public interface IcategoryRepository
    {
        Task<OperationResult<bool>> AddCategory(AddCategoryDto dto);
        Task<OperationResult<bool>> UpdateCategory(UpdateCategoryDto dto);
        Task<OperationResult<List<GetCategoryDto>>> GetAllCategory();
        Task<OperationResult<GetCategoryDto>> GetCategoryById(Guid Id);
        Task<OperationResult<bool>> DeleteCategory(Guid id);
    }
}
