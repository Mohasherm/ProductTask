using Microsoft.AspNetCore.Mvc;
using ProductTask.Repository.Permission.Dto;
using ProductTask.Repository.Permission;
using ProductTask.Shared.Enums;
using ProductTask.Utill;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ButterflyApi.Base.ErrorHandling;

namespace ProductTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        #region Prioperties and constructors

        private IContentRepository _contentRepository { get; }
        public PermissionController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }
        #endregion

        #region Post

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(bool))]
        [DynamicAuthorize(nameof(ControllerNames.Permission), nameof(RequestType.Set))]
        public async Task<IActionResult> SetPermission(SetPermissionDto dto)
        {
            var result = await _contentRepository.SetPermissionsAsync(dto);
            return result.GetResult();
        }
        #endregion

        #region Get

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(RoleContentsDto))]
        //[DynamicAuthorize(nameof(ControllerNames.Permission), nameof(RequestType.Get))]
        public async Task<IActionResult> GetContentsByRoleId(Guid roleId)
        {
            var result = await _contentRepository.GetContentsByRoleIdAsync(roleId);
            return result.GetResult();
        }


        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<RoleContentsDto>))]
        [DynamicAuthorize(nameof(ControllerNames.Permission), nameof(RequestType.Get))]
        public async Task<IActionResult> GetRolesContentsA()
        {
            var result = await _contentRepository.GetRolesContentsAsync();
            return result.GetResult();
        }
        #endregion
    }
}
