using Microsoft.AspNetCore.Authorization;
using CompanyWorkspaceService.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Constants;

namespace CompanyWorkspaceService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"]
                ?? throw new InvalidOperationException("Secret not configured");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                
                options.AddPolicy(Polices.MustBeAdministrator,
                    policy => policy.RequireRole(Roles.Administrator));

                options.AddPolicy(Polices.MustBeSellerOrAdministrator,
                    policy => policy.RequireRole(Roles.Administrator, Roles.Seller));
                
                options.AddPolicy(Polices.MustBeInRole,
                    policy => policy.RequireRole(Roles.Buyer, Roles.Seller, Roles.Administrator));

            });

            services.AddExceptionHandler<ExceptionsMiddleware>();

            return services;
        }
    }
}
