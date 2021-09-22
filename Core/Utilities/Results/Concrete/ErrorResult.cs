using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorResult:ResultBase
    {
        public ErrorResult() : base(false)
        {
        }

        public ErrorResult(params string[] messages) : base(false, messages)
        {
        }
    }
}
