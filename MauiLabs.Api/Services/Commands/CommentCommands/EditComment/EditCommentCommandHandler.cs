using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MauiLabs.Api.Services.Commands.CommentCommands.EditComment
{
    public partial class EditCommentCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<EditCommentCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var commentFilter = (Comment p) => p.ProfileId == request.ProfileId && p.RecipeId == request.RecipeId;
            var requestType = typeof(EditCommentCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var comment = await dbcontext.Comments.FirstOrDefaultAsync(item => commentFilter.Invoke(item));
                if (comment == null) throw new ApiServiceException("Комментарий не найден", requestType);

                comment.Rating = request.Rating;
                comment.Text = request.Text;
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
