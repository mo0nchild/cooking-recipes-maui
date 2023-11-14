using FluentValidation;
using MediatR;
using System.Runtime.CompilerServices;

namespace MauiLabs.Api.Services.Commons.Exceptions
{
    public partial class ApiServiceException: ValidationException
    {
        public Type ServiceType { get; private set; } = default!;
        public string ErrorMessage { get => this.Message; }
        public ApiServiceException(string message, Type type) : base(message) => this.ServiceType = type;
    }
}
