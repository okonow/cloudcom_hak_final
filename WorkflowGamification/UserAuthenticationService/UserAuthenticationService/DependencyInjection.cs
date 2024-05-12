using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using UserAuthenticationService.Common.Constants;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Data;
using UserAuthenticationService.Middlewares;
using UserAuthenticationService.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationString = configuration.GetConnectionString("DbConnection");
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configurationString);
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(dataSource);
            });

            var secret = configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");
            var result = int.TryParse(configuration["JWT:RefreshTokenExpireInDays"], out int expiredTime);

            if (!result)
                throw new InvalidOperationException("time must be number");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.ExpireTimeSpan = TimeSpan.FromMicroseconds(expiredTime);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Polices.MustBeAdministrator, p => p.RequireRole(Roles.Administrator));
            });

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User authentication",
                    Description = "About Application"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                    }
                });
            });

            services.AddSingleton(TimeProvider.System);
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            services.AddExceptionHandler<ExceptionsHandlerMiddleware>();
            services.AddScoped<IAuthorizationMiddlewareResultHandler, AuthenticationMiddleware>();

            return services;
        }
    }
}
