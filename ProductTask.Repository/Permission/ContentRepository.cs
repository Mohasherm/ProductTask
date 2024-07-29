using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ProductTask.Base.OperationResult;
using ProductTask.Entity.Permission;
using ProductTask.Entity.Security;
using ProductTask.Repository.Base;
using ProductTask.Repository.Permission.Dto;
using ProductTask.Shared.Enums;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTask.Repository.Permission
{
    public class ContentRepository : BaseRepository, IContentRepository
    {

        #region Properties and constructors

        public ContentRepository(DataContext _context, IHttpContextAccessor _http_contextAccessor) : base(_context, _http_contextAccessor)
        {

        }
        #endregion

        #region Set

        public async Task<OperationResult<bool>> SetPermissionsAsync(SetPermissionDto dto)
        {
            var result = new OperationResult<bool>();

            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    #region validation

                    if (!await IsExist<RoleModel>(m => m.Id == dto.RoleId))
                    {
                        result.AddError(ErrorKey.RoleNotFound, ResultStatus.NotFound);
                        return result;
                    }

                    if (dto.Contents == null || dto.Contents.Count == 0)
                    {
                        result.AddError(ErrorKey.PLeaseChooseContents, ResultStatus.ValidationError);
                        return result;
                    }

                    var contentsIds = dto.Contents.Select(e => e.Id).ToList();
                    var someContentNotFound = await _context.WebContents.AnyAsync(e => !contentsIds.Contains(e.Id));

                    if (someContentNotFound)
                    {
                        result.AddError(ErrorKey.SomeContentNotFound, ResultStatus.NotFound);
                        return result;
                    }

                    var role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == dto.RoleId);
                    if (role.Name == Role.Admin.ToString())
                    {
                        result.AddError(ErrorKey.CannotUpdateAdminRolePermission, ResultStatus.NotFound);
                        return result;
                    }
                    #endregion

                    var contentRoleEntities = new List<WebContentRoleModel>();
                    var EntityExist = true;
                    var contents = await _context.WebContents.ToListAsync();
                    var contentRoles = await _context.WebContentRoles.ToListAsync();

                    var entitiesToAdd = new List<WebContentRoleModel>();
                    var entitiesToUpd = new List<WebContentRoleModel>();

                    foreach (var content in dto.Contents)
                    {
                        content.CanView = content.CanAdd || content.CanDelete || content.CanEdit || content.CanView ? true : false;

                        var contentRoleEntity = contentRoles.FirstOrDefault(entity => entity.RoleId == dto.RoleId
                        && entity.WebContentId == content.Id);

                        if (contentRoleEntity is null)
                        {
                            EntityExist = false;
                            contentRoleEntity = new WebContentRoleModel();
                        }
                        contentRoleEntity.WebContentId = content.Id;
                        contentRoleEntity.RoleId = dto.RoleId;
                        contentRoleEntity.CanAdd = content.CanAdd;
                        contentRoleEntity.CanDelete = content.CanDelete;
                        contentRoleEntity.CanEdit = content.CanEdit;
                        contentRoleEntity.CanView = content.CanView;
                        if (EntityExist)
                        {
                            entitiesToUpd.Add(contentRoleEntity);
                        }
                        else
                        {
                            EntityExist = true;
                            entitiesToAdd.Add(contentRoleEntity);
                        }
                    }

                    if (entitiesToAdd.Any())
                        await _context.WebContentRoles.AddRangeAsync(entitiesToAdd);

                    if (entitiesToUpd.Any())
                        _context.WebContentRoles.UpdateRange(entitiesToUpd);

                    await _context.SaveChangesAsync();

                    result.Data = true;
                    await trans.CommitAsync();
                    return result;
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    result.AddError(ErrorKey.InternalServerError, ResultStatus.InternalServerError);
                    return result;
                }
            }
        }
        #endregion

        #region Get
        public OperationResult<IEnumerable<string>> GetContentRoles(string contentName, string action)
        {
            var result = new OperationResult<IEnumerable<string>>();
            try
            {
                var data = _context.WebContentRoles
                              .Where(entity => entity.WebContent.Name == contentName 
                               && (action == nameof(RequestType.Get) && entity.CanView
                                     || action == nameof(RequestType.Set) && (entity.CanAdd || entity.CanEdit)
                                     || action == nameof(RequestType.Delete) && entity.CanDelete)
                                     )
                              .Select(entity => entity.Role.Name)
                              .ToList();
                result.Data = data ?? new List<string>();
            }
            catch (Exception)
            {
                result.AddError(ErrorKey.InternalServerError, ResultStatus.InternalServerError);
                return result;
            }
            return result;
        }

        public async Task<OperationResult<RoleContentsDto>> GetContentsByRoleIdAsync(Guid roleId)
        {
            var result = new OperationResult<RoleContentsDto>();
            try
            {
                var data = await _context.Roles.Where(e => e.Id == roleId).IgnoreQueryFilters()
                     .Select(e => new RoleContentsDto()
                     {
                         RoleId = e.Id,
                         RoleName = e.Name,
                         Contents = e.WebContentRoles.Where(e => e.IsValid)
                         .Select(e => new ContentDto
                         {
                             Id = e.WebContentId,
                             Name = e.WebContent.Name,
                             CanAdd = e.CanAdd,
                             CanEdit = e.CanEdit,
                             CanDelete = e.CanDelete,
                             CanView = e.CanView
                         }).ToList()
                     }).FirstOrDefaultAsync();
                result.Data = data;
            }
            catch (Exception)
            {
                result.AddError(ErrorKey.InternalServerError, ResultStatus.InternalServerError);
                return result;
            }
            return result;
        }

        public async Task<OperationResult<IEnumerable<RoleContentsDto>>> GetRolesContentsAsync()
        {
            var result = new OperationResult<IEnumerable<RoleContentsDto>>();
            try
            {
                var data = await _context.Roles
                     .Select(e => new RoleContentsDto()
                     {
                         RoleId = e.Id,
                         RoleName = e.Name,
                         Contents = e.WebContentRoles.Where(e => e.IsValid
                         && (e.CanView || e.CanDelete || e.CanAdd || e.CanEdit))
                         .Select(e => new ContentDto
                         {
                             Id = e.WebContentId,
                             Name = e.WebContent.Name,
                             CanAdd = e.CanAdd,
                             CanEdit = e.CanEdit,
                             CanDelete = e.CanDelete,
                             CanView = e.CanView
                         }).ToList()
                     }).ToListAsync();
                result.Data = data;
            }
            catch (Exception)
            {
                result.AddError(ErrorKey.InternalServerError, ResultStatus.InternalServerError);
                return result;
            }
            return result;
        }
        #endregion
    }
}
