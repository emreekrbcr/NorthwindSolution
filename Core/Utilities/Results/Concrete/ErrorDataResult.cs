using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResultBase<T>
    {
        public ErrorDataResult() : base(default, false)
        {
        }

        public ErrorDataResult(params string[] messages) : base(default, false, messages)
        {
        }
    }
}
