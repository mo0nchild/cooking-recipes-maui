using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.CommentCommands.DeleteComment
{
    public partial class DeleteCommentCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteCommentCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var deleteFilter = (Comment item) => item.RecipeId == request.RecipeId && item.ProfileId == request.ProfileId;

            var requestType = typeof(DeleteCommentCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.Comments.Where(item => deleteFilter.Invoke(item)).ExecuteDeleteAsync();
                if (result <= 0) throw new ApiServiceException("Комментарий не найден", requestType);
            }
        }
    }
}
