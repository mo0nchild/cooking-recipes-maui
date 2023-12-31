﻿using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList
{
    public partial class GetPublishedRecipeListRequest : IRequest<CookingRecipesList>
    {
        public required int PublisherId { get; set; } = default!;
        public string? TextFilter { get; set; } = default;
        public string? Category { get; set; } = default!;
    }
}
