using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Result<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }

        public Result(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static Result<T> SuccessResult(T data, string message = "İşlem başarılı.") =>
            new Result<T>(true, message, data);

        public static Result<T> FailureResult(string message) =>
            new Result<T>(false, message);
    }
}
