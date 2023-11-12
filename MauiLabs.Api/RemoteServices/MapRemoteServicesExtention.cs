using MauiLabs.Api.RemoteServices.Implementations.CookingRecipe;
using MauiLabs.Api.RemoteServices.Implementations.FriendsList;
using MauiLabs.Api.RemoteServices.Implementations.RecommendsList;
using Microsoft.Extensions.Options;

namespace MauiLabs.Api.RemoteServices
{
    public static class MapRemoteServicesExtention : object
    {
        public static IEndpointRouteBuilder MapRemoteServices(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGrpcService<CookingRecipeServiceCommands>();
            routeBuilder.MapGrpcService<CookingRecipeServiceRequests>();

            routeBuilder.MapGrpcService<FriendsListServiceCommands>();
            routeBuilder.MapGrpcService<FriendsListServiceRequests>();

            routeBuilder.MapGrpcService<RecommendsListServiceCommands>();
            routeBuilder.MapGrpcService<RecommendsListServiceRequests>();

            routeBuilder.MapGrpcReflectionService();
            return routeBuilder;
        }
    }
}
