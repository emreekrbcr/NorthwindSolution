using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Bu metod sayesinde CoreModule'ın içine yazdığımız çözümlemelerden başka bir modül daha yazarsak polimorfik olarak ekleyebilelim diye.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection,
            params ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}