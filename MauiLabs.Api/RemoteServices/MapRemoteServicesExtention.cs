using Calzolari.Grpc.AspNetCore.Validation;
using MauiLabs.Api.RemoteServices.Implementations.CookingRecipe;
using MauiLabs.Api.RemoteServices.Implementations.FriendsList;
using MauiLabs.Api.RemoteServices.Implementations.RecommendsList;
using Microsoft.Extensions.Options;

namespace MauiLabs.Api.RemoteServices
{
    public static class RemoteServicesExtension : object
    {
        public static Task<IServiceCollection> AddRemoteServices(this IServiceCollection collection, IConfiguration configuration) 
        {
            collection.AddGrpc(options => options.EnableMessageValidation().EnableDetailedErrors = true);
            collection.AddGrpcReflection().AddGrpcValidation();
            
            collection.AddValidator<AddRecommendModel.ModelValidation>(ServiceLifetime.Scoped);
            collection.AddValidator<AddRecommendByTokenModel.ModelValidation>(ServiceLifetime.Scoped);

            collection.AddValidator<AddFriendByTokenModel.ModelValidation>(ServiceLifetime.Scoped);
            collection.AddValidator<AddFriendModel.ModelValidation>(ServiceLifetime.Scoped);

            collection.AddValidator<AddCookingRecipeByTokenModel.ModelValidation>(ServiceLifetime.Scoped);
            collection.AddValidator<EditCookingRecipeModel.ModelValidation>(ServiceLifetime.Scoped);
            return Task.FromResult(collection);
        }

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
