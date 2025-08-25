using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.Repositories.Common;
using JiJiBotApp_Backend.Repositories.Company;
using JiJiBotApp_Backend.Repositories.Login;
using JiJiBotApp_Backend.Repositories.Permission;
using JiJiBotApp_Backend.Repositories.Repositories;
//using JiJiBotApp_Backend.Repositories.Employee;
using JiJiBotApp_Backend.Repositories.Role;
using JiJiBotApp_Backend.Repositories.RolePermissionAssociation;
using JiJiBotApp_Backend.Repositories.Screen;
using JiJiBotApp_Backend.Repositories.ScreenAssociation;
using JiJiBotApp_Backend.Repositories.User;
using JiJiBotApp_Backend.Repositories.UserRepoAssociation;
using JiJiBotApp_Backend.Repositories.UserRoleAssociation;
using JiJiBotApp_Backend.Services.Common;
using JiJiBotApp_Backend.Services.Company;
using JiJiBotApp_Backend.Services.Login;
using JiJiBotApp_Backend.Services.Permission;
using JiJiBotApp_Backend.Services.Repositories;
using JiJiBotApp_Backend.Services.Repository;
using JiJiBotApp_Backend.Services.Role;
using JiJiBotApp_Backend.Services.RolePermissionAssociation;
using JiJiBotApp_Backend.Services.Screen;
using JiJiBotApp_Backend.Services.ScreenAssociation;
using JiJiBotApp_Backend.Services.User;
using JiJiBotApp_Backend.Services.UserRepoAssociation;
using JiJiBotApp_Backend.Services.UserRoleAssociation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
//using JiJiBotApp_Backend.Services.Shop;
//using JiJiBotApp_Backend.Repositories.Shop;
//using JiJiBotApp_Backend.Services.Employee;
using System.Runtime.CompilerServices;
//using JiJiBotApp_Backend.Repositories.Client;
//using JiJiBotApp_Backend.Services.Client;
using System.Text;

namespace JiJiBotApp_Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Setup JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false; // dev/testing me disable
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                        };
                    });


            // Register common services
            services.AddSingleton<IStoredProcedureExecutor, StoredProcedureExecutor>();

            // Register repositories
         
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            //services.AddScoped<IClientRepository,ClientRepository>();
            //services.AddScoped<IShopRepository, ShopRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleAssociationRepository, UserRoleAssociationRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRepositoriesRepository, RepositoriesRepository>();
            services.AddScoped<IRolePermissionAssociationRepository, RolePermissionAssociationRepository>();
            services.AddScoped<IUserRepoAssociationRepository, UserRepoAssociationRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IScreenRepository, ScreenRepository>();
            services.AddScoped<IScreenAssociationRepository, ScreenAssociationRepository>();

            // Register services

            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ICompanyService, CompanyService>();
            //services.AddScoped<IClientService, ClientService>();
            //services.AddScoped<IEmployeeService, EmployeeService>();
            //services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleAssociationService, UserRoleAssociationService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRepositoriesService, RepositoryService>();
            services.AddScoped<IRolePermissionAssociationService, RolePermissionAssociationService>();
            services.AddScoped<IUserRepoAssociationService, UserRepoAssociationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IScreenService, ScreenService>();
            services.AddScoped<IScreenAssociationService, ScreenAssociationService>();

            return services;
        }
    }
}
