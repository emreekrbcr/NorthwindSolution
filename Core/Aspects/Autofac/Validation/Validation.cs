using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class Validation : MethodInterceptorBaseAttribute
    {
        private readonly Type _validatorType;
        private readonly IValidator _validator;

        public Validation(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Invalid validator type error.");
            }
            _validatorType = validatorType;
            _validator = (IValidator)Activator.CreateInstance(_validatorType); //Bunu OnBefore'un içine yazarsak aynı metod için her validation işlemi yapılacağı sıra bellekte bir nesne oluşturur gerek yok
        }

        //Örneğin veritabanına bir ürün ekleyeceğiz. Add metodu çalışmadan önce validation işlemleri yapılsın diye OnBefore metodunu eziyoruz
        protected override void OnBefore(IInvocation invocation)
        {
            //Runtime'da _validatorType ProductValidator gelmiş olsun. Git onun BaseType'ını bul yani AbstractValidator<Product>. Onun da generic type'larını bul. Bir tane old. için dizinin 0.elemanındadır ve o da Product'dır!
            Type entityType = _validatorType.BaseType.GetGenericArguments()[0];

            //Metodun parametrelerini al. Örneğin Add metodunun parametresi Product product olsun(List<Product>'da yani çoğul da olabilir). Başka parametreleri de olsun. Parametresinin tipi validator type'ın base type'ındaki tipse yani Product'sa doğrulama yapacaz.
            var entities = invocation.Arguments.Where(a => a.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(_validator, entity);
            }
        }
    }
}