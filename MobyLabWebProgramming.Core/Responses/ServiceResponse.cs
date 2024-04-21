using MobyLabWebProgramming.Core.Errors;

namespace MobyLabWebProgramming.Core.Responses;

/// <summary>
/// These classes are used as responses from service methods as either a success responses or as error responses.
/// </summary>
public class ServiceResponse
{
    public ErrorMessage? Error { get; set; }
    public bool IsOk => Error == null;

    public static ServiceResponse FromError(ErrorMessage? error) => new() { Error = error };
    public static ServiceResponse ForSuccess() => new();

    protected ServiceResponse()
    {
    }
}

public class ServiceResponse<T> : ServiceResponse
{
    public T? Result { get; set; }

    public new static ServiceResponse<T> FromError(ErrorMessage? error) => new() { Error = error };
    public static ServiceResponse<T> ForSuccess(T data) => new() { Result = data };
    public ServiceResponse ToResponse() => ServiceResponse.FromError(Error);

    private ServiceResponse()
    {
    }
}
