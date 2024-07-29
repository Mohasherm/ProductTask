using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ProductTask.Repository.Permission;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProductTask.Utill
{
    public class DynamicAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        #region Properties and constructors

        private string _contentName { get; set; }
        private string _action { get; set; }

        public DynamicAuthorizeAttribute(string contentName, string action)
        {
            _contentName = contentName;
            _action = action;
        }
        #endregion

        #region Methods

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var contentRepository = context.HttpContext.RequestServices.GetRequiredService<IContentRepository>();
            var result = contentRepository.GetContentRoles(_contentName, _action);
            var roles = string.IsNullOrEmpty(result.ErrorMessage.ToString()) ? new List<string>()
                : result.Data;
            var rolesString = string.Join(",", roles);
            var role = string.Empty;
            var _httpContextAccessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            var _httpcontext = _httpContextAccessor.HttpContext;
            if (_httpcontext != null
                    && _httpcontext.User != null
                    && _httpcontext.User.Identity != null
                    && _httpcontext.User.Identity.IsAuthenticated)
            {
                role = _httpcontext.User.FindFirstValue(ClaimTypes.Role);
            }

            if (rolesString == null || rolesString.Count() == 0)
            {
                context.Result = new UnauthorizedResult();
            }
            else if (!roles.Contains(role))
            {
                context.Result = new ForbidResult();
            }
        }
        #endregion
    }
}
