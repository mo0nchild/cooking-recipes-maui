using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Commons
{
    public partial class ViewServiceException : Exception
    {
        public HttpStatusCode ExceptionType { get; protected set; } = default!;
        public ViewServiceException(string message, HttpStatusCode errorType) : base(message) 
        {
            this.ExceptionType = errorType;
        }
        public ViewServiceException(string message) : this(message, HttpStatusCode.BadRequest) { }
    }
}
