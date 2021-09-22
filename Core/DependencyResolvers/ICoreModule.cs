using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection); //Her proje için ortak olan bağımlılıkları yükleyecek(Api'deki)
    }
}
