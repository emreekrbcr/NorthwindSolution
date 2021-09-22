using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static IResult Run(params IResult[] rules)
        {
            List<string>errorMessages=new List<string>();

            foreach (var rule in rules)
            {
                if (!rule.Success)
                {
                    errorMessages.Add(rule.Messages[0]);
                }
            }

            if (errorMessages.Count>0)
            {
                return new ErrorResult(errorMessages.ToArray());
            }

            return new SuccessResult();
        }
    }
}