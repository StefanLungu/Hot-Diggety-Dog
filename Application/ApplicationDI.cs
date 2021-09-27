using Microsoft.Extensions.Configuration;
using Application.Features.OrderFeatures.Services;
using Application.Interfaces;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Application.Settings;
using Application.Features;

namespace Application
{
    public static class ApplicationDI
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IOrdersService), typeof(OrdersService))
                    .AddScoped(typeof(IEmailService), typeof(EmailService))
                    .AddScoped(typeof(IHtmlService), typeof(HtmlService))
                    .AddMediatR(Assembly.GetExecutingAssembly())
                    .Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        }
    }
}
