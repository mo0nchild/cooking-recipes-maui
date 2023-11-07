using MauiLabs.Api.Services.Requests.IngredientItemRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.IngredientItemRequests.GetIngredientItems
{
    public partial class GetIngredientItemsRequest : IRequest<IngredientItemsCollection>
    {
        public GetIngredientItemsRequest(): base() { }
    }
}
