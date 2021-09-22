using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemove:MethodInterceptorBaseAttribute
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemove(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) //örneğin ekle, güncelleme, silme işlemleri gibi veri manipülasyonu yapan metodlar başarılı olursa tüm get içeren metodları cache'den uçur gibi bir kullanım yapacağımız için bu metodlar başarılı olursa çalışacak
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
