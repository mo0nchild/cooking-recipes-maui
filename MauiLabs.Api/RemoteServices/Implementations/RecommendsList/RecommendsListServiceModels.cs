using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;

namespace MauiLabs.Api.RemoteServices.Implementations.RecommendsList
{
    public partial class AddRecommendModel : IMappingTarget<AddRecommendCommand> { }
    public partial class AddRecommendByTokenModel : IMappingTarget<AddRecommendCommand> { }
    public partial class DeleteRecommendModel : IMappingTarget<DeleteRecommendCommand> { }

    public partial class GetRecommendsListByTokenModel : IMappingTarget<GetRecommendsListRequest> { }
    public partial class GetRecommendsListModel : IMappingTarget<GetRecommendsListRequest> { }

}
