using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.CommentCommands.AddComment
{
    public partial class AddCommentCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<AddCommentCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = _mapper.Map<Comment>(request);
            mappedModel.PublicationTime = DateTime.UtcNow;
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == mappedModel.ProfileId) == null)
                {
                    throw new ApiServiceException("Профиль не найден", typeof(AddCommentCommand));
                }
                if (await dbcontext.CookingRecipes.FirstOrDefaultAsync(item => item.Id == mappedModel.RecipeId) == null)
                {
                    throw new ApiServiceException("Рецепт не найден", typeof(AddCommentCommand));
                }
                var collision = await dbcontext.Comments.FirstOrDefaultAsync(item => item.RecipeId == request.RecipeId 
                    && item.ProfileId == request.ProfileId);
                if (collision != null) throw new ApiServiceException("Комментарий уже добавлен", typeof(AddCommentCommand));

                await dbcontext.Comments.AddRangeAsync(mappedModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
