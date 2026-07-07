using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappings;
using ProductManagement.Application.Services;
using ProductManagement.Application.Validators;

namespace ProductManagement.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));

            services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
 