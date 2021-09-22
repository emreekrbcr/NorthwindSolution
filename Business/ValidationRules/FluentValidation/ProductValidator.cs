using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(3);
            RuleFor(p => p.ProductName).Must(StartsWithA).When(p => p.CategoryId == 2).WithMessage("Ürün adı 'A' harfi ile başlamalı.");

            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1); //CategoryId'si 1 olan örneğin içecekler için minimum birim fiyatı 10 lira olabilir gibi bir validation

            RuleFor(p => p.UnitsInStock).NotEmpty();
        }

        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("a") || arg.StartsWith("A");
        }
    }
}
