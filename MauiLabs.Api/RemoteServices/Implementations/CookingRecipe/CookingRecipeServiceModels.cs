using AutoMapper;
using FluentValidation;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.ConfirmeCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.DeleteCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.Models;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipe;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using static MauiLabs.Api.RemoteServices.Implementations.CookingRecipe.AddCookingRecipeByTokenModel;

namespace MauiLabs.Api.RemoteServices.Implementations.CookingRecipe
{
    public partial class AddCookingRecipeModel : IMappingTarget<AddCookingRecipeCommand>
    {
        public sealed class ModelValidation : AbstractValidator<AddCookingRecipeModel>
        {
            public ModelValidation() : base()
            {
                this.RuleFor(item => item.Name).NotEmpty()
                    .MaximumLength(50).WithMessage("Неверное значение названия рецепта: длина до 50 символов")
                    .MinimumLength(3).WithMessage("Неверное значение названия рецепта: длина от 3 символов");
                this.RuleFor(item => item.Category)
                    .MaximumLength(50).WithMessage("Неверное значение названия категории: длина до 50 символов");

                this.RuleFor(item => item.Ingredients).ForEach(item =>
                {
                    item.ChildRules(p => p.RuleFor(i => i.Key).NotEmpty()
                        .MaximumLength(50).WithMessage("Неверное значение названия ингредиента: длина до 50 символов")
                        .MinimumLength(3).WithMessage("Неверное значение названия ингредиента: длина от 3 символов"));
                    item.ChildRules(p => p.RuleFor(i => i.Value.Unit).NotEmpty()
                        .MaximumLength(20).WithMessage("Неверное значение названия единицы измерения: длина до 20 символов"));
                });
            }
        }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ByteString, byte[]?>().ConvertUsing(item => item.ToByteArray());
            profile.CreateMap<Types.IngredientUnitModel, IngredientUnitInfo>();
            profile.CreateMap<AddCookingRecipeModel, AddCookingRecipeCommand>();
        }
    }
    public partial class AddCookingRecipeByTokenModel : IMappingTarget<AddCookingRecipeCommand>
    {
        public sealed class ModelValidation : AbstractValidator<AddCookingRecipeByTokenModel>
        {
            public ModelValidation() : base()
            {
                this.RuleFor(item => item.Name).NotEmpty()
                    .MaximumLength(50).WithMessage("Неверное значение названия рецепта: длина до 50 символов")
                    .MinimumLength(3).WithMessage("Неверное значение названия рецепта: длина от 3 символов");
                this.RuleFor(item => item.Category)
                    .MaximumLength(50).WithMessage("Неверное значение названия категории: длина до 50 символов");

                this.RuleFor(item => item.Ingredients).ForEach(item =>
                {
                    item.ChildRules(p => p.RuleFor(i => i.Key).NotEmpty()
                        .MaximumLength(50).WithMessage("Неверное значение названия ингредиента: длина до 50 символов")
                        .MinimumLength(3).WithMessage("Неверное значение названия ингредиента: длина от 3 символов"));
                    item.ChildRules(p => p.RuleFor(i => i.Value.Unit).NotEmpty()
                        .MaximumLength(20).WithMessage("Неверное значение названия единицы измерения: длина до 20 символов"));
                });
            }
        }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ByteString, byte[]?>().ConvertUsing(item => item.ToByteArray());
            profile.CreateMap<Types.IngredientUnitModel, IngredientUnitInfo>();
            profile.CreateMap<AddCookingRecipeByTokenModel, AddCookingRecipeCommand>();
        }
    }
    public partial class ConfirmeCookingRecipeModel : IMappingTarget<ConfirmeCookingRecipeCommand> { }
    public partial class DeleteCookingRecipeModel : IMappingTarget<DeleteCookingRecipeCommand> { }
    public partial class EditCookingRecipeModel : IMappingTarget<EditCookingRecipeCommand> 
    {
        public sealed class ModelValidation : AbstractValidator<EditCookingRecipeModel>
        {
            public ModelValidation() : base()
            {
                this.RuleFor(item => item.Name).NotEmpty()
                    .MaximumLength(50).WithMessage("Неверное значение названия рецепта: длина до 50 символов")
                    .MinimumLength(3).WithMessage("Неверное значение названия рецепта: длина от 3 символов");
                this.RuleFor(item => item.Category)
                    .MaximumLength(50).WithMessage("Неверное значение названия категории: длина до 50 символов");

                this.RuleFor(item => item.Ingredients).ForEach(item =>
                {
                    item.ChildRules(p => p.RuleFor(i => i.Key).NotEmpty()
                        .MaximumLength(50).WithMessage("Неверное значение названия ингредиента: длина до 50 символов")
                        .MinimumLength(3).WithMessage("Неверное значение названия ингредиента: длина от 3 символов"));
                    item.ChildRules(p => p.RuleFor(i => i.Value.Unit).NotEmpty()
                        .MaximumLength(20).WithMessage("Неверное значение названия единицы измерения: длина до 20 символов"));
                });
            }
        }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ByteString, byte[]?>().ConvertUsing(item => item.ToByteArray());
            profile.CreateMap<Types.IngredientUnitModel, IngredientUnitInfo>();
            profile.CreateMap<EditCookingRecipeModel, EditCookingRecipeCommand>();
        }
    }

    public partial class GetCookingRecipeModel : IMappingTarget<GetCookingRecipeRequest> 
    {
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<GetCookingRecipeModel, GetCookingRecipeRequest>();
        }
    }
    public partial class GetCookingRecipesListModel : IMappingTarget<GetCookingRecipesListRequest> 
    {
        public sealed class ModelValidation : AbstractValidator<GetCookingRecipesListModel>
        {
            public ModelValidation() : base()
            {
                this.RuleFor(item => item.Skip).Must(item => item >= 0).WithMessage("Значение [Skip] не может быть отрицательным");
                this.RuleFor(item => item.Take).Must(item => item >= 0).WithMessage("Значение [Take] не может быть отрицательным");
                this.RuleFor(item => item.SortingType).IsInEnum().WithMessage("Неверное значение сортировки");
            }
        }

        public void ConfigureMapping(Profile profile) 
        {
            profile.CreateMap<Types.RecipeSortingType, RecipeSortingType>().ConvertUsing(item => (RecipeSortingType)item);
            profile.CreateMap<GetCookingRecipesListModel, GetCookingRecipesListRequest>();
        }
    }
    public partial class GetPublishedRecipeListModel : IMappingTarget<GetPublishedRecipeListRequest> { }
    public partial class GetPublisherRecipeListByTokenModel : IMappingTarget<GetPublishedRecipeListRequest> { }

    public partial class CookingRecipeListModel : IMappingTarget<CookingRecipesList> { }
    public partial class CookingRecipeInfoModel : IMappingTarget<CookingRecipeInfo> 
    {
        public static partial class Types
        {
            public sealed partial class PublisherInfoModel : IMappingTarget<PublisherInfo> 
            {
                public void ConfigureMapping(Profile profile)
                {
                    profile.CreateMap<byte[]?, ByteString>().ConvertUsing(item => ByteString.CopyFrom(item));
                    profile.CreateMap<PublisherInfo, PublisherInfoModel>();
                }
            }
            public sealed partial class IngredientInfoModel : IMappingTarget<IngredientInfo> { }
        }

        public void ConfigureMapping(Profile profile) 
        {
            profile.CreateMap<DateTime, Timestamp>().ConvertUsing(item => Timestamp.FromDateTime(item));
            profile.CreateMap<byte[]?, ByteString>().ConvertUsing(item => ByteString.CopyFrom(item));
            profile.CreateMap<CookingRecipeInfo, CookingRecipeInfoModel>();
        }
    }
}
