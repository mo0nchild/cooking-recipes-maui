using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend
{
    public partial class AddRecommendCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<AddRecommendCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(AddRecommendCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(AddRecommendCommand);
            var collisionFilter = (Recommendation el) =>
            {
                return el.ToUserId == request.ToUserId && el.FromUserId == request.FromUserId && el.RecipeId == request.RecipeId;
            };
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == request.FromUserId) == null)
                {
                    throw new ApiServiceException("Профиль отправителя не найден", requestType);
                }
                if (await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == request.ToUserId) == null)
                {
                    throw new ApiServiceException("Профиль получателя не найден", requestType);
                }
                if (await dbcontext.CookingRecipes.FirstOrDefaultAsync(item => item.Id == request.RecipeId) == null)
                {
                    throw new ApiServiceException("Рецепт не найден", requestType);
                }
                var collision = await dbcontext.Recommendations.FirstOrDefaultAsync(item => collisionFilter.Invoke(item));
                if (collision != null) throw new ApiServiceException("Рекомендация уже отправлена", requestType);

                await dbcontext.Recommendations.AddRangeAsync(this._mapper.Map<Recommendation>(request));
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
