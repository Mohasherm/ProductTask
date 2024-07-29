using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductTask.Base.OperationResult;
using ProductTask.Entity.Main;
using ProductTask.Repository.Base;
using ProductTask.Repository.Main.Dto;
using ProductTask.Shared.Enums;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTask.Repository.Main
{
    public class categoryRepository : BaseRepository, IcategoryRepository
    {
        public categoryRepository(DataContext context, IHttpContextAccessor httpContextAccessor = null) : base(context, httpContextAccessor)
        {
        }

        public async Task<OperationResult<bool>> AddCategory(AddCategoryDto dto)
        {
            var res = new OperationResult<bool>();

            if (string.IsNullOrEmpty(dto.Name))
                res.ThrowException(ErrorKey.SomeFieldesIsRequired,ResultStatus.ValidationError);

            var data = new CategoryModel
            {
                Name = dto.Name,
            };

            _context.Add(data);
            _context.SaveChanges();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<bool>> DeleteCategory(Guid id)
        {
            var res = new OperationResult<bool>();

            if (id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<CategoryModel>(x => x.Id == id);

            if (data == null)
                res.ThrowException(ErrorKey.CategoryNotFound, ResultStatus.ValidationError);

            data.IsValid = false;

            _context.SaveChanges();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<List<GetCategoryDto>>> GetAllCategory()
        {
            var res = new OperationResult<List<GetCategoryDto>>();

            res.Data = await _context.Categories
                .AsNoTracking()
                .Select(x => new GetCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();

            return res;
        }

        public async Task<OperationResult<GetCategoryDto>> GetCategoryById(Guid Id)
        {
            var res = new OperationResult<GetCategoryDto>();

            if (Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            res.Data = await _context.Categories
               .AsNoTracking()
               .Select(x => new GetCategoryDto
               {
                   Id = x.Id,
                   Name = x.Name,
               }).FirstOrDefaultAsync(x => x.Id == Id);

            return res;
        }

        public async Task<OperationResult<bool>> UpdateCategory(UpdateCategoryDto dto)
        {
            var res = new OperationResult<bool>();

            if (dto.Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<CategoryModel>(x => x.Id == dto.Id);

            if (data == null)
                res.ThrowException(ErrorKey.CategoryNotFound, ResultStatus.ValidationError);

            data.Name =dto.Name;

            _context.SaveChanges();

            res.Data = true;
            return res;
        }
    }
}
