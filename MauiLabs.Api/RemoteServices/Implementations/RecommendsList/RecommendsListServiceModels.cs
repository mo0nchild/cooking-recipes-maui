using AutoMapper;
using FluentValidation;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using Microsoft.Extensions.Options;

namespace MauiLabs.Api.RemoteServices.Implementations.RecommendsList
{
    public partial class AddRecommendModel : IMappingTarget<AddRecommendCommand> 
    {
        public sealed class ModelValidation : AbstractValidator<AddRecommendModel>
        {
            public ModelValidation() : base()
            {
                this.RuleFor(item => item.Text).NotEmpty()
                    .MinimumLength(10).WithMessage("Неверное значение текста рекомендации: длина от 10 символов")
                    .MaximumLength(200).WithMessage("Неверное значение текста рекомендации: длина до 200 символов");
            }
        }
    }
    public partial class AddRecommendByTokenModel : IMappingTarget<AddRecommendCommand> 
    {
        public sealed class ModelValidation : AbstractValidator<AddRecommendByTokenModel>
        {
            public ModelValidation() : base()
            {
                this.RuleFor(item => item.Text).NotEmpty()
                    .MinimumLength(10).WithMessage("Неверное значение текста рекомендации: длина от 10 символов")
                    .MaximumLength(200).WithMessage("Неверное значение текста рекомендации: длина до 200 символов");
            }
        }
    }
    public partial class DeleteRecommendModel : IMappingTarget<DeleteRecommendCommand> { }

    public partial class GetRecommendsListByTokenModel : IMappingTarget<GetRecommendsListRequest> { }
    public partial class GetRecommendsListModel : IMappingTarget<GetRecommendsListRequest> { }

    
    public partial class RecommendInfoModel : IMappingTarget<RecommendInfo> 
    {
        public static partial class Types : object
        {
            public partial class ProfileInfoModel : IMappingTarget<ProfileInfo>
            {
                public void ConfigureMapping(Profile profile)
                {
                    profile.CreateMap<byte[]?, ByteString>().ConvertUsing(item => ByteString.CopyFrom(item));
                    profile.CreateMap<ProfileInfo, ProfileInfoModel>();
                }
            }
            public partial class CookingRecipeInfoModel : IMappingTarget<CookingRecipeInfo>
            {
                public static partial class Types : object
                {
                    public partial class IngredientInfoModel : IMappingTarget<IngredientInfo> { }
                    public sealed partial class PublisherInfoModel : IMappingTarget<PublisherInfo>
                    {
                        public void ConfigureMapping(Profile profile)
                        {
                            profile.CreateMap<PublisherInfo, PublisherInfoModel>();
                            profile.CreateMap<byte[]?, ByteString>().ConvertUsing(item => ByteString.CopyFrom(item));
                        }
                    }
                }

                public void ConfigureMapping(Profile profile)
                {
                    profile.CreateMap<DateTime, Timestamp>().ConvertUsing(item => Timestamp.FromDateTime(item));
                    profile.CreateMap<byte[]?, ByteString>().ConvertUsing(item => ByteString.CopyFrom(item));
                    profile.CreateMap<CookingRecipeInfo, CookingRecipeInfoModel>();
                }
            }
        }
    }
}
