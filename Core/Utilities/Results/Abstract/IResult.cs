namespace Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        public bool Success { get; }
        public string[] Messages { get; }
    }
}
