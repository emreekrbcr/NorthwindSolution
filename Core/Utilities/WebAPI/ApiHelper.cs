using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Core.Utilities.WebAPI
{
    public static class ApiHelper
    {
        public static IActionResult CheckRequestResult(IResult result)
        {
            if (result.Success)
            {
                return new OkObjectResult(result);
            }

            return new BadRequestObjectResult(result);
        } 
    }
}