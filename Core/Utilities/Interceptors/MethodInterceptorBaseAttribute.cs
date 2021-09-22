using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    //AllowMultiple=true olmasını sebebi örneğin loglama işlemi yapacağız ve veritabanı,dosya,sms gibi birden fazla kaynak ile loglama yapabiliriz ondan dolayı.

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptorBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        //Alttaki metodların abstract değil de virtual olmasının sebebi örneğin Aspects/Validation/ValidationAspect.cs'nin içerisinde sadece OnBefore metodunu ezmek istememizdir. Yani abstract olsaydı orada veya diğer CCC'lerde tüm metodları ezmemiz gerekecekti. Ama bu şekilde spesifik olarak metodları ezebiliyoruz!

        //invocation çalıştırmak istediğimiz metod anlamına gelmektedir. Örneğin Business katmanında Add metodunun üzerine bir Validation attribute'u koyduğumuzda işte o Add metodu invocation'dır.

        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception exception) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }

        public virtual void Intercept(IInvocation invocation)
        {
            bool isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                isSuccess = false;
                OnException(invocation, exception);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}