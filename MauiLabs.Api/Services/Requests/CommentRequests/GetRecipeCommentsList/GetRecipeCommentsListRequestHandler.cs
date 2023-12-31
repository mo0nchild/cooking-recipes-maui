﻿using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList
{
    public partial class GetRecipeCommentsListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetRecipeCommentsListRequest, CommentsList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CommentsList> Handle(GetRecipeCommentsListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = dbcontext.Comments.Where(item => item.RecipeId == request.RecipeId)
                    .Include(item => item.Profile);
                var filtredResult = await requestResult.Skip(request.Skip).Take(request.Take).ToListAsync();
                var sortedResult = (request.SortingType switch
                {
                    CommentSortingType.ByRating => filtredResult.OrderByDescending(item => item.Rating),
                    CommentSortingType.ByDate => filtredResult.OrderByDescending(item => item.PublicationTime),
                    _ => throw new ApiServiceException("Не установлен режим сортировки", typeof(GetRecipeCommentsListRequest)),
                })
                .ToImmutableList();
                return new CommentsList()
                {
                    Comments = _mapper.Map<List<CommentInfo>>(sortedResult),
                    AllCount = await requestResult.CountAsync(),
                };
            }
        }
    }
}
