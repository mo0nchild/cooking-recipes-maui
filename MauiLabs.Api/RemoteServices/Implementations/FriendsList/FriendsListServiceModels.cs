using AutoMapper;
using FluentValidation;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.FriendCommands.AddFriend;
using MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;

namespace MauiLabs.Api.RemoteServices.Implementations.FriendsList
{
    public partial class AddFriendModel : IMappingTarget<AddFriendCommand>
    {
        public sealed class ModelValidation : AbstractValidator<AddFriendModel>
        {
            public ModelValidation() : base() 
            {
                this.RuleFor(item => item.ReferenceLink).NotEmpty()
                    .MinimumLength(5).WithMessage("Неверное значение ссылки: длина от 5 символов")
                    .MaximumLength(72).WithMessage("Неверное значение ссылки: длина от 72 символов");
            }
        }
    }
    public partial class AddFriendByTokenModel : IMappingTarget<AddFriendCommand>
    {
        public sealed class ModelValidation : AbstractValidator<AddFriendByTokenModel>
        {
            public ModelValidation() : base() 
            {
                this.RuleFor(item => item.ReferenceLink).NotEmpty()
                    .MinimumLength(5).WithMessage("Неверное значение ссылки: длина от 5 символов")
                    .MaximumLength(72).WithMessage("Неверное значение ссылки: длина от 72 символов");
            }
        }
        
    }
    public partial class DeleteFriendModel : IMappingTarget<DeleteFriendCommand> { }

    public partial class GetFriendsListModel : IMappingTarget<GetFriendsListRequest> { }

    public partial class FriendInfoModel : IMappingTarget<FriendInfo> 
    {
        public static partial class Types : object
        {
            public partial class ProfileInfoModel : IMappingTarget<ProfileInfo>
            {
                public void ConfigureMapping(Profile profile)
                {
                    profile.CreateMap<DateTime, Timestamp>().ConvertUsing(item => Timestamp.FromDateTime(item));
                    profile.CreateMap<byte[]?, ByteString>().ConvertUsing(item => ByteString.CopyFrom(item));
                    profile.CreateMap<ProfileInfo, ProfileInfoModel>();
                }
            }
        }
    }
    
}
