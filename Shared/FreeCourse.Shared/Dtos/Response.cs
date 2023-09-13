﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }

        [JsonIgnore] //status code zaten response alırken bizde olacak ekstra olarak response body'de almıyoruz
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public List<string> Errors { get; set; }

        public static Response<T> Success(T data, int statusCode) //static factory methods
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessfull = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessfull = true };
        }
        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }
        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessfull = false };
        }
    }
}
