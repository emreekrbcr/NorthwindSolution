namespace Core.Utilities.Results.Abstract
{
    public abstract class ResultBase : IResult
    {
        public bool Success { get; }
        public string[] Messages { get; }

        public ResultBase(bool success)
        {
            Success = success;
        }

        public ResultBase(bool success, params string[] messages) : this(success)
        {
            Messages = messages;
        }
    }
}
