using ProductTaskFrontEnd.Service.Prodeuct.Dto;

namespace ProductTaskFrontEnd.Service.Prodeuct;

public interface IProductService
{
    Task<GetResult<bool>> AddProduct(AddProduct dto);
    Task<GetResult<bool>> UpdateProduct(UpdateProduct dto);
    Task<GetResult<bool>> DeleteProduct(Guid Id);
    Task<GetResult<List<GetProductsDto>>> GetAllProducts();
    Task<GetResult<List<GetProductsDto>>> GetAllProductsByCategoryId(Guid? Id);
    Task<GetResult<UpdateProduct>> GetProductById(Guid Id);
}
