using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList
{
    public partial class GetRecommendsListRequest : IRequest<RecommendInfoList>
    {
        public required int ProfileId { get; set; } = default!; 
    }
}
