using MauiLabs.Api.Services.Commands.CommentCommands.AddComment;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark
{
    public partial class AddBookmarkCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<AddBookmarkCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(AddBookmarkCommand request, CancellationToken cancellationToken)
        {
            var filterCollision = (Bookmark p) => p.ProfileId == request.ProfileId && p.RecipeId == request.RecipeId;
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == request.ProfileId) == null)
                {
                    throw new ApiServiceException("Профиль не найден", typeof(AddCommentCommand));
                }
                if (await dbcontext.CookingRecipes.FirstOrDefaultAsync(item => item.Id == request.RecipeId) == null)
                {
                    throw new ApiServiceException("Рецепт не найден", typeof(AddCommentCommand));
                }
                if (await dbcontext.Bookmarks.FirstOrDefaultAsync(item => filterCollision.Invoke(item)) != null)
                {
                    throw new ApiServiceException("Заметка уже добавлена", typeof(AddCommentCommand));
                }
                await dbcontext.Bookmarks.AddRangeAsync(new Bookmark()
                {
                    ProfileId = request.ProfileId, RecipeId = request.RecipeId,
                    AddTime = DateTime.UtcNow,
                });
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
