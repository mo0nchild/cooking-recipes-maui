using MauiLabs.Api.Services.Commons.Models;

namespace MauiLabs.Api.Services.Commons.Interfaces
{
    public interface IRequestLogging
    {
        public Task LogRequest(LogRequestMessage message);
    }
}
