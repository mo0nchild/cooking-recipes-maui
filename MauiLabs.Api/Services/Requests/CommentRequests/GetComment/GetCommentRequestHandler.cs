using AutoMapper;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetComment
{
    public partial class GetCommentRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetCommentRequest, CommentInfo?>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CommentInfo?> Handle(GetCommentRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var comment = await dbcontext.Comments.Include(item => item.Profile)
                    .FirstOrDefaultAsync(item => item.ProfileId == request.ProfileId && item.RecipeId == request.RecipeId);

                return this._mapper.Map<CommentInfo?>(comment);
            }
        }
    }
}
