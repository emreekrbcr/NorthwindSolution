using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class Performance:MethodInterceptorBaseAttribute
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public Performance(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds>_interval)
            {
                Debug.WriteLine($"Performance Warning : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --> {_stopwatch.Elapsed.TotalSeconds} second");
                //Burası şimdilik konsola yazdırır istersek mail at sms gönder vb. şeyler de yapabiliriz
            }
            _stopwatch.Reset();
        }
    }
}
