using ClotheStore.Application.Commands;
using ClotheStore.Application.Queries;
using ClotheStore.Application.Services;
using ClotheStore.Domain.Repositories;
using ClotheStore.Domain.Services;
using ClotheStore.Repository.Context;
using ClotheStore.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClotheStore.Api.Extensions
{
    public static class DIExtension
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Authorization Services
            services.AddScoped<IIdentityService, IdentityService>();

            // Query Services
            services.AddScoped<IUserQueryService, UserQueryService>();
            services.AddScoped<IContactPreferenceQueryService, ContactPreferenceQueryService>();
            services.AddScoped<IAddressQueryService, AddressQueryService>();
            services.AddScoped<ICartItemQueryService, CartItemQueryService>();
            services.AddScoped<IOptionQueryService, OptionQueryService>();

            // Command Services
            services.AddScoped<IUserCommandService, UserCommandService>();
            services.AddScoped<IContactPreferenceCommandService, ContactPreferenceCommandService>();
            services.AddScoped<IAddressCommandService, AddressCommandService>();
            services.AddScoped<ICartItemCommandService, CartItemCommandService>();
            services.AddScoped<ICartCommandService, CartCommandService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<ITrackActionRepository, TrackActionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITrackActionRepository, TrackActionRepository>();
            services.AddScoped<IContactPreferenceRepository, ContactPreferenceRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            return services;
        }
    }
}
