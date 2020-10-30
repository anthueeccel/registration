using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using register.app.AutoMapper;
using register.app.Interfaces;
using register.app.Services;
using register.domain.CommandHandler;
using register.domain.Commands;
using register.domain.Interfaces;
using register.domain.Mediator;
using register.domain.Messaging;
using register.infra.data.Repositories;

namespace register.Api
{
    public class ServiceRegistrator
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelAutoMapper));

            //Mediator
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            //Domain - Commands
            services.AddScoped<IRequestHandler<AddCustomerCommand, Unit>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, Unit>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, Unit>, CustomerCommandHandler>();

            //Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
