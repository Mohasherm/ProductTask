using ButterflyApi.SharedKernal.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductTask.Base.OperationResult;
using ProductTask.Entity.Security;
using ProductTask.Repository.Account.Dto;
using ProductTask.Repository.Base;
using ProductTask.Repository.Security.Token;
using ProductTask.Repository.Security.Token.Dto;
using ProductTask.Shared.Enums;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTask.Repository.Account
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly ITokenRepository tokenRepository;
        public AccountRepository(DataContext context, IHttpContextAccessor httpContextAccessor = null, ITokenRepository tokenRepository = null) : base(context, httpContextAccessor)
        {
            this.tokenRepository = tokenRepository;
        }

     

        public async Task<OperationResult<bool>> DeleteUser(Guid Id)
        {
            var res = new OperationResult<bool>();

            if (Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var data = await _get<UserModel>(x => x.Id == Id);

            if (data == null)
                res.ThrowException(ErrorKey.UserNotFound, ResultStatus.ValidationError);

            data.IsValid = false;
            await _context.SaveChangesAsync();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<List<GetAllUsersResponse>>> GetAllUsers()
        {
            var res = new OperationResult<List<GetAllUsersResponse>>();

            res.Data = await _context.Users
                      .AsNoTracking()
                      .Select(x => new GetAllUsersResponse
                      {
                          Id = x.Id,
                          Name = x.UserName,
                          PhoneNumber = x.PhoneNumber,
                          Email = x.Email
                      }).ToListAsync();

            return res;
        }

        public async Task<OperationResult<List<GetRolesDto>>> GetRoles()
        {
            var result = new OperationResult<List<GetRolesDto>>();

            result.Data = await _context.Roles
                .AsNoTracking()
                .Select(x => new GetRolesDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return result;
        }

        public async Task<OperationResult<GetAllUsersResponse>> GetUserById(Guid Id)
        {
            var res = new OperationResult<GetAllUsersResponse>();

            if (Id == Guid.Empty)
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            if (!await IsExist<UserModel>(x => x.Id == Id))
                res.ThrowException(ErrorKey.UserNotFound, ResultStatus.ValidationError);

            res.Data = await _context.Users
                      .AsNoTracking()
                      .Select(x => new GetAllUsersResponse
                      {
                          Id = x.Id,
                          Name = x.UserName,
                          PhoneNumber = x.PhoneNumber,
                          Email = x.Email
                      }).FirstOrDefaultAsync(x => x.Id == Id);

            return res;
        }

        public async Task<OperationResult<TokenDto>> Login(LoginRequest request)
        {
            var res = new OperationResult<TokenDto>();

            #region Validation
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var user = await _context.Users.FirstOrDefaultAsync(s => s.UserName == request.UserName);

            if (user is null || !PasswordHelper.VerifyPassword(request.Password, user.Password))
                res.ThrowException(ErrorKey.UserNameOrPasswordIsInvalid, ResultStatus.ValidationError);

            #endregion
            var token = await tokenRepository.GenerateJwtToken(user.Id);
            res.Data = new TokenDto
            {
                Token = token.Token,
                UserId = token.UserId,
                RefreshToken = token.RefreshToken,
            };

            return res;
        }

        public async Task<OperationResult<bool>> Register(RegisterRequest request)
        {
            var res = new OperationResult<bool>();

            if (await IsExist<UserModel>(s => s.UserName == request.UserName))
                res.ThrowException(ErrorKey.UserNameHasBeenTaken, ResultStatus.ValidationError);

            if (string.IsNullOrEmpty(request.Password))
                res.ThrowException(ErrorKey.SomeFieldesIsRequired, ResultStatus.ValidationError);

            var cpuser = new UserModel
            {
                RoleId = _context.Roles.FirstOrDefault(x => x.Name == nameof(Role.User)).Id,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = PasswordHelper.HashPassword(request.Password)
            };
            _context.Users.Add(cpuser);
            await _context.SaveChangesAsync();

            res.Data = true;
            return res;
        }

        public async Task<OperationResult<bool>> Update(UpdateRequest request)
        {
            var res = new OperationResult<bool>();

            if (await IsExist<UserModel>(s => s.UserName == request.UserName && s.Id != GetUserId()))
                res.ThrowException(ErrorKey.UserNameHasBeenTaken, ResultStatus.ValidationError);

            var user = await _get<UserModel>(x => x.Id == GetUserId());

            user.UserName = request.UserName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = PasswordHelper.HashPassword(request.Password);
            }
            await _context.SaveChangesAsync();

            res.Data = true;
            return res;
        }
    }
}
