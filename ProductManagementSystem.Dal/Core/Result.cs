namespace ProductManagementSystem.Dal.Core;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; }
    public string Error { get; set; }
    public int StatusCode { get; set; }

    public static Result<T> Success(T value, int statusCode = 200) => new() { IsSuccess = true, Value = value, StatusCode = statusCode };
    public static Result<T> Failure(string error, int statusCode) => new() { IsSuccess = false, Error = error, StatusCode = statusCode };
}