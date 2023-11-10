using MauiLabs.Api.Services.Requests.IngredientRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.IngredientRequests.GetIngredientUnits
{
    public partial class GetIngredientUnitsRequest : IRequest<IngredientUnitsCollection>
    {
        public GetIngredientUnitsRequest() : base() { }
    }
}
