using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.BookmarkCommands.DeleteBookmark
{
    public partial class DeleteBookmarkCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteBookmarkCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteBookmarkCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.Bookmarks.Where(item => item.ProfileId == request.ProfileId 
                    && item.RecipeId == request.RecipeId).ExecuteDeleteAsync();

                if (result <= 0) throw new ApiServiceException("Заметка не найдена", requestType);
            }
        }
    }
}
