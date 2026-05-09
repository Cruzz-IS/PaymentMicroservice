using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PaymentsSystem.Application.Interfaces;
using PaymentsSystem.Infrastructure.Persistence;
using PaymentsSystem.Infrastructure.Persistence.Repositories;
using PaymentsSystem.Application.Commands.CreateCustomer;
using PaymentsSystem.Application.Commands;

namespace PaymentMicroservice.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.EnableRetryOnFailure(5)));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    typeof(CreateCustomerCommand).Assembly));

            services.AddValidatorsFromAssembly(
                typeof(CreateCustomerCommandValidator).Assembly);

            return services;
        }
    }
}
