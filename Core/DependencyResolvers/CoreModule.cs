using System.Diagnostics;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Bu çözümlemeyi burada ekle aşağıda ise ServiceTool'u çağırmamız gerekiyor aksi takdirde Program.cs'nin içerisinde çözümleyici olarak Autofac'i kullandığımız ve sisteme entegre ettiğimiz için sistem buradaki çözümlemeleri yapamaz
            //Bunu normalde Startup.cs'in içine eklemiştik ama sonradan tüm projelerde kullanılabilecek bir çözümleme old. için Core'a aldık 
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddMemoryCache(); //MemoryCacheManager'ın içindeki IMemoryCache'in enjeksiyonu

            serviceCollection.AddTransient<Stopwatch>(); //Engin Hoca burayı AddSingleton olarak yapmış ancak API'ye bir dünya istek geldiğinde bunlara sadece bir tane Stopwatch nesnesi bakmak zorunda kalacağı için süreleri tutamayacaktı. AddTransient normal bir şekilde new Stopwatch() oluşturmak gibi yani metod her çağrıldığında o metoda özel kronometre oluşturulacak 
        }
    }
}
