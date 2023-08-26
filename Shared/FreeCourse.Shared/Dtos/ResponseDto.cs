using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        [JsonIgnore] //status code zaten response alırken bizde olacak ekstra olarak response body'de almıyoruz
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode) //static factory methods
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessfull = true };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default, StatusCode = statusCode, IsSuccessfull = true };
        }
        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessfull = false };
        }
    }
}
