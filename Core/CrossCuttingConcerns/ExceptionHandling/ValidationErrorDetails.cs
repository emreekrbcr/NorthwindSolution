using System.Collections.Generic;
using FluentValidation.Results;

namespace Core.CrossCuttingConcerns.ExceptionHandling
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}