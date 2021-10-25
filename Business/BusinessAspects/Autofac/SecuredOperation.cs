using System;
using Business.Utilities.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterceptorBaseAttribute
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        //Client'ten API'ye JWT göndererek bir istek yaparız. Her istek gönderen için bir HttpContext oluşur. Yani her istek için bir thread oluşur. Bunu onun için kullanacaz.

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //API'nin ctor'una gidip injection yaptık, IoC'de hazır API'de orada bir sıkıntı yok. Ancak projeyi bir WindowsForm Application'da kullanmak istersek ServiceTool'u ve içine yazdığımız kodları kullanmamız gerekir. Bu tool'u kullananarak herhangi bir interface'ın karşılığını alabiliriz

            //Burada kullanma sebebimiz ise bağımlılık zinciri içerisinde Aspect yok. Burada injection yapmaya çalışırsak başarılı olamayız. Yani Autofac'den IHttpContextAccessor nesnesini karşılığını alabilmemiz için bu toolu kullanmamız gerekmektedir. Windows Form tarafında olsaydık örneğin: _productService=ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); diyerek bunun karşılığını alabilirdik.
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }

            throw new Exception(Messages.AuthMessages.ErrorMessages.AuthorizationDenied);
        }
    }
}