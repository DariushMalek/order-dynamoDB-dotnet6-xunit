using System.ComponentModel.DataAnnotations;

namespace TechLand.Shop.Model.Response;

public class ResponseResult
{
    public ResponseResult()
    {
        ValidationResult = ValidationResult.Success;
    }

    public ResponseResult(object result)
    {
        Result = result;
        ValidationResult = ValidationResult.Success;
    }

    public ResponseResult(string errorMessage)
    {
        ValidationResult = new ValidationResult(errorMessage);
    }

    public object Result { get; set; }

    public ValidationResult? ValidationResult { get; set; }

    public bool IsSuccess()
    {
        return ValidationResult == null;
    }

    public T GetReuslt<T>()
    {
        return (T)Result;
    }
} 