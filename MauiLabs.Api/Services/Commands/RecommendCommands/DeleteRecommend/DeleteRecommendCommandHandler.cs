using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend
{
    public partial class DeleteRecommendCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteRecommendCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(DeleteRecommendCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteRecommendCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var resultRequest = await dbcontext.Recommendations.Where(item => item.Id == request.RecordId).ExecuteDeleteAsync();
                if (resultRequest <= 0) throw new ApiServiceException("Рекомендация не найдена", requestType);
            }
        }
    }
}
