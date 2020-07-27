using System;

namespace RuletaOnline.ExceptionMiddlewares
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 400;
        public object Value { get; set; }

        public HttpResponseException(object error)
        {
            Value = error;
        }
    }
}