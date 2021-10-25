using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //Bir metod çalışacağı sıra ilgili sınıfın üzerindeki attribute'ları alır. Daha sonra metodun üzerindekileri de alıp, classAttributes'a ekler. Bunları priority'sine göre sıralayarak bir IInterceptor dizisi olarak geriye döndürür.
            var classAttributes = type.GetCustomAttributes<MethodInterceptorBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptorBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //burayı sonra merkezi loglama yönetimi için kullancaz, şimdilik gerek yok
            
            classAttributes.Add(new Performance(5)); //tüm sistemde bir metodun çalışma süresi 5 sn'yi geçerse bize bilgilendirme yapar

            return classAttributes.OrderBy(i => i.Priority).ToArray();
        }
    }
}