using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessResult:ResultBase
    {
        public SuccessResult() : base(true)
        {
        }

        public SuccessResult(params string[] messages) : base(true, messages)
        {
        }
    }
}
