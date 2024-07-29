using ProductTask.Base.OperationResult;
using ProductTask.Repository.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.Repository.Permission
{
    public interface IContentRepository
    {
        #region Set

        Task<OperationResult<bool>> SetPermissionsAsync(SetPermissionDto dto);
        #endregion

        #region Get

        Task<OperationResult<RoleContentsDto>> GetContentsByRoleIdAsync(Guid roleId);
        Task<OperationResult<IEnumerable<RoleContentsDto>>> GetRolesContentsAsync();
        OperationResult<IEnumerable<string>> GetContentRoles(string contentName, string action);
        #endregion
    }
}
