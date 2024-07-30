using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductTask.Base.OperationResult;
using ProductTask.Entity.Main;
using ProductTask.Repository.Base;
using ProductTask.Repository.Main.Product.Dto;
using ProductTask.Shared.Enums;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTask.Repository.Main.Product
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(DataContext context, IHttpContextAccessor httpContextAccessor = null) : base(context, httpContextAccessor)
        {
        }

        public async Task<OperationResult<bool>> AddProduct(AddProduct dto)
        {
            var res = new OperationResult<bool>();

            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Description) || dto.Price <= 0)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            if (dto.Categories != null && dto.Categories.Count > 0)
            {
                foreach (var category in dto.Categories)
                {
                    var cat = await _get<CategoryModel>(x => x.Id == category);
                    if (cat == null)
                    {
                        res.ThrowException(ErrorKey.ThereIsSomeCategoriesNotFound, ResultStatus.ValidationError);
                    }
                }
            }

            var data = new ProductModel
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ProductCategories = dto.Categories?.Select(x => new ProductCategoryModel { CategoryId = x }).ToList(),
            };

            _context.Add(data);
            _context.SaveChanges();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<bool>> UpdateProduct(UpdateProduct dto)
        {
            var res = new OperationResult<bool>();

            if (dto.Id == Guid.Empty || string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Description) || dto.Price <= 0)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<ProductModel>(x => x.Id == dto.Id);

            if (data == null)
                res.ThrowException(ErrorKey.ProductNotFound, ResultStatus.ValidationError);

            if (dto.Categories != null && dto.Categories.Count > 0)
            {
                foreach (var category in dto.Categories)
                {
                    var cat = await _get<CategoryModel>(x => x.Id == category);
                    if (cat == null)
                    {
                        res.ThrowException(ErrorKey.ThereIsSomeCategoriesNotFound, ResultStatus.ValidationError);
                    }
                }
            }

            var productcategory = _context.ProductCategories.Where(x => x.ProductId == data.Id).ToList();

            _context.RemoveRange(productcategory);


            data.Name = dto.Name;
            data.Description = dto.Description;
            data.Price = dto.Price;
            data.ProductCategories = dto.Categories?.Select(x => new ProductCategoryModel { CategoryId = x }).ToList();

            _context.SaveChanges();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<bool>> DeleteProduct(Guid Id)
        {
            var res = new OperationResult<bool>();

            if (Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<ProductModel>(x => x.Id == Id);

            if (data == null)
                res.ThrowException(ErrorKey.ProductNotFound, ResultStatus.ValidationError);

            data.IsValid = false;
            _context.SaveChanges();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<List<GetProductsDto>>> GetAllProducts()
        {
            var res = new OperationResult<List<GetProductsDto>>();

            res.Data = await _context.Products
                .AsNoTracking()
                .Select(x => new GetProductsDto
                {
                    Id = x.Id,
                    Name = x.Name,  
                    Description = x.Description,
                    Price = x.Price
                }).ToListAsync();

            return res;
        }

        public async Task<OperationResult<List<GetProductsDto>>> GetAllProductsByCategoryId(Guid Id)
        {
            var res = new OperationResult<List<GetProductsDto>>();

            if (Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<CategoryModel>(x => x.Id == Id);

            if (data == null)
                res.ThrowException(ErrorKey.CategoryNotFound, ResultStatus.ValidationError);

            res.Data = await _context.ProductCategories
                .AsNoTracking()
                .Where(x => x.CategoryId == Id)
                .Select(x => new GetProductsDto
                {
                    Id = x.ProductId,
                    Name = x.Product.Name,
                    Description = x.Product.Description,
                    Price = x.Product.Price
                }).ToListAsync();

            return res;
        }

        public async Task<OperationResult<UpdateProduct>> GetProductById(Guid Id)
        {
            var res = new OperationResult<UpdateProduct>();

            if (Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<ProductModel>(x => x.Id == Id);

            if (data == null)
                res.ThrowException(ErrorKey.ProductNotFound, ResultStatus.ValidationError);

            res.Data = await _context.Products
                         .AsNoTracking()
                         .Select(x => new UpdateProduct
                         {
                             Id = x.Id,
                             Name = x.Name,
                             Description = x.Description,
                             Price = x.Price,
                             Categories = x.ProductCategories.Select(x => x.CategoryId).ToList()
                         }).FirstOrDefaultAsync(x => x.Id == Id);
            return res;
        }
    }
}
