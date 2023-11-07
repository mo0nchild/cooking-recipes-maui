using MauiLabs.Api.Services.Requests.RecipeCategoryRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.RecipeCategoryRequests.GetRecipeCategoryList
{
    public partial class GetCategoryListRequest : IRequest<CategoryInfoList>
    {
        public GetCategoryListRequest() : base() { }
    }
}
