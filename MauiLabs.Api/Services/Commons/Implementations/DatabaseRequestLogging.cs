using AutoMapper;
using MauiLabs.Api.Services.Commons.Interfaces;
using MauiLabs.Api.Services.Commons.Models;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commons.Implementation
{
    public partial class DatabaseRequestLogging : IRequestLogging
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = default!;
        protected readonly IMapper _mapper = default!;

        protected ILogger<DatabaseRequestLogging> Logger { get; private set; } = default!;
        public DatabaseRequestLogging(IDbContextFactory<CookingRecipeDbContext> factory, IMapper mapper) : base() 
        {
            (this._factory, this._mapper) = (factory, mapper);
            this.Logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<DatabaseRequestLogging>();
        }
        public virtual async Task LogRequest(LogRequestMessage message)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync())
            {
                await dbcontext.LoggingInfos.AddAsync(this._mapper.Map<LoggingInfo>(message));
                await dbcontext.SaveChangesAsync();
            }
            this.Logger.LogInformation($"Access Info: [ UserIP: {message.UserInfo  }; Date/Time: {message.DateTime}; " +
                $"MethodName: {message.MethodName} ]");
        }
    }
}
