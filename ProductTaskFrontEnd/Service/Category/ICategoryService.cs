using ProductTaskFrontEnd.Service.Category.Dto;

namespace ProductTaskFrontEnd.Service.Category;

public interface ICategoryService
{
    Task<GetResult<bool>> AddCategory(AddCategoryDto dto);
    Task<GetResult<bool>> UpdateCategory(UpdateCategoryDto dto);
    Task<GetResult<List<GetCategoryDto>>> GetAllCategory();
    Task<GetResult<GetCategoryDto>> GetCategoryById(Guid Id);
    Task<GetResult<bool>> DeleteCategory(Guid id);
}
