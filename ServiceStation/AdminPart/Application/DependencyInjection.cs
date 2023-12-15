using System.Reflection;
using Application.Configurations;
using Application.Factories.Interfaces;
using Application.Factories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using GeneralBusMessages.Message;
using Application.EventBusConsumers;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<JwtTokenConfiguration>();
            services.AddScoped<IJwtSecurityTokenFactory, JwtSecurityTokenFactory>();

            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
/*            services.AddMassTransit(opt =>
            {
                opt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c => {
                    c.ReceiveEndpoint(nameof(Model), e =>
                    {
                        e.ConfigureConsumer<ModelConsumer>(context);
                    });
                
                }));
            });*/

            return services;
        }



    }
}
