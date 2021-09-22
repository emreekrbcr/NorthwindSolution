namespace Core.Utilities.Results.Abstract
{
    public abstract class DataResultBase<T> : ResultBase, IDataResult<T>
    {
        public T Data { get; }

        public DataResultBase(T data, bool success) : base(success)
        {
            Data = data;
        }

        public DataResultBase(T data, bool success, params string[] messages) : base(success, messages)
        {
            Data = data;
        }
    }
}
