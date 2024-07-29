using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductTask.Entity.Base;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductTask.Repository.Base
{
    public class BaseRepository
    {
        #region Ctor
        protected DataContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepository(DataContext context, IHttpContextAccessor httpContextAccessor = null)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        public string acceptLanguage
        {
            get
            {
                return GetAcceptedLanguage();
            }
        }

        public string GetAcceptedLanguage()
        {
            var currentLanguage = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                currentLanguage = _httpContextAccessor.HttpContext.Request
                   .Headers["accept-language"];

                /* allAcceptLanguage = _httpContextAccessor.HttpContext.Request
                     .Headers["accept-language"];*/
            }

            if (string.IsNullOrEmpty(currentLanguage) ||
                currentLanguage.ToString().StartsWith("en"))
            {
                return "en";
            }
            else if (currentLanguage.ToString().StartsWith("ar"))
            {
                return "ar";
            }
            else
            {
                return "en";
            }

        }
        public Guid GetUserId()
        {
            Guid id = Guid.Empty;
            try
            {
                var _httpcontext = _httpContextAccessor.HttpContext;
                if (_httpcontext != null
                        && _httpcontext.User != null
                        && _httpcontext.User.Identity != null
                        && _httpcontext.User.Identity.IsAuthenticated)
                {
                    id = Guid.Parse(_httpcontext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
            }
            catch (Exception)
            {
                id = Guid.Empty;
            }
            return id;
        }
        public string GetUserRole()
        {
            try
            {
                var _httpcontext = _httpContextAccessor.HttpContext;
                if (_httpcontext != null
                        && _httpcontext.User != null
                        && _httpcontext.User.Identity != null
                        && _httpcontext.User.Identity.IsAuthenticated)
                {
                    return (_httpcontext.User.FindFirstValue(ClaimTypes.Role));
                }
            }
            catch (Exception)
            {
            }
            return "";
        }


        #region Entity Methods
        public async Task<TEntity> _get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity
        {
            return await _context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity
            => await _context.Set<TEntity>().Where(filter).AnyAsync();
        #endregion

    }
}
