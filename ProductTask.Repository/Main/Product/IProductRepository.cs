using ProductTask.Base.OperationResult;
using ProductTask.Repository.Main.Product.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.Repository.Main.Product
{
    public interface IProductRepository
    {
        Task<OperationResult<bool>> AddProduct(AddProduct dto);
        Task<OperationResult<bool>> UppdateProduct(UpdateProduct dto);
        Task<OperationResult<bool>> DeleteProduct(Guid Id);
        Task<OperationResult<List<GetProductsDto>>> GetAllProducts();
        Task<OperationResult<List<GetProductsDto>>> GetAllProductsByCategoryId(Guid Id);
        Task<OperationResult<UpdateProduct>> GetProductById(Guid Id);
    }
}
