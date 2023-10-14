namespace KwikDeploy.Application.Common.Models;

public class ResultWithId<T>
{
    internal ResultWithId(T id, bool succeeded, IEnumerable<string> errors)
    {
        Id = id;
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    internal ResultWithId(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public T? Id { get; set; }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static ResultWithId<T> Success(T id)
    {
        return new ResultWithId<T>(id, true, Array.Empty<string>());
    }

    public static ResultWithId<T> Failure(IEnumerable<string> errors)
    {
        return new ResultWithId<T>(false, errors);
    }
}