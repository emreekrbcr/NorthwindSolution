using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResultBase<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(T data, params string[] messages) : base(data, true, messages)
        {
        }
    }
}