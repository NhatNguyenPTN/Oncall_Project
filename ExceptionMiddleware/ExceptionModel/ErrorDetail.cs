using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionMiddleware.ExceptionModel
{
    class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
