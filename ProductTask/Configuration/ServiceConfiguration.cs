using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductTask.Repository.Account;
using ProductTask.Repository.Main;
using ProductTask.Repository.Security.Token;
using ProductTask.SqlServer.Data;
using ProductTask.Utill;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ProductTask.Configuration
{
    public static class ServiceConfiguration
	{
		public static IServiceCollection AddDbContextService<TDbContext>(this IServiceCollection services, string connectionString)
			   where TDbContext : DataContext
        {
			services.AddDbContext<TDbContext>(options =>
						 options.UseSqlServer(connectionString));

			return services;
		}
		public static IServiceCollection AddRepositoriesService(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddHttpClient();
			services.AddScoped<ITokenRepository, TokenRepository>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<IcategoryRepository, categoryRepository>();



			return services;
		}
		public static IServiceCollection AddMiddlewaresService(this IServiceCollection services)
		{
			services.AddTransient<HandleExceptionMiddleware>();

			return services;
		}

		public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, string jwtKey)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.Events = new JwtBearerEvents
				{
					OnChallenge = context =>
					{
						context.HandleResponse();
						context.Response.StatusCode = 401;
						return Task.CompletedTask;
					}
				};
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateActor = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
				};
			});
			return services;
		}
		public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Task API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Enter JWT Bearer token **_only_**",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
                        new string[]{}
					}
				});
			});
			return services;
		}

	}
}
