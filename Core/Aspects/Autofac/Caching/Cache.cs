using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class Cache:MethodInterceptorBaseAttribute
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public Cache(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //Bunu CoreModule'ün içinde çözdüğümüz için yarın öbür gün başka bir caching sistemine geçtiğimizde otomatik oradan görecek burada değişiklik yapmamıza gerek yok.
        }

        public override void Intercept(IInvocation invocation) //bunu OnBefore ile de yapabilirdik ama böyle de yapabiliriz
        {
            //ReflectedType namespace'ini almak demek
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            if (_cacheManager.CheckIfInCache(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key); //cache'de varsa metod hangi veritipini döndürüyorsa onu al ve bitir 
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}