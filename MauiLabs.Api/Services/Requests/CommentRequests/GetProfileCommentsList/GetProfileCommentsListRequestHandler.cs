using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetProfileCommentsList
{
    public partial class GetProfileCommentsListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetProfileCommentsListRequest, CommentsList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CommentsList> Handle(GetProfileCommentsListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = dbcontext.Comments.Where(item => item.ProfileId == request.ProfileId)
                    .Include(item => item.Profile);
                var sortedResult = await (request.SortingType switch
                {
                    CommentSortingType.ByRating => requestResult.OrderByDescending(item => item.Rating),
                    CommentSortingType.ByDate => requestResult.OrderByDescending(item => item.PublicationTime),
                    _ => throw new ApiServiceException("Не установлен режим сортировки", typeof(GetRecipeCommentsListRequest)),
                })
                .Skip(request.Skip).Take(request.Take).ToListAsync();
                return new CommentsList()
                {
                    Comments = this._mapper.Map<List<CommentInfo>>(sortedResult),
                    AllCount = await requestResult.CountAsync(),
                };
            }
        }
    }
}
