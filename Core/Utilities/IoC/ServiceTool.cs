using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        //.Net'in servislerini al ve onları build et. Web API'de veya Autofac'de oluşturduğumuz injectionların karşılıklarını almamıza yarıyor
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
