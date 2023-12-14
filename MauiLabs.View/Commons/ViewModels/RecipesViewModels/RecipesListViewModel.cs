using MauiLabs.View.Services.ApiModels.Commons.RecipeModels;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.RecipesViewModels
{
    public partial class RecipesListViewModel : INotifyPropertyChanged
    {
        public static readonly string DefaultRecipeImage = $"MauiLabs.View.Resources.Images.Recipe.defaultrecipe.jpg";
        public static readonly string DefaultSortingType = "По дате";
        public static readonly string DefaultCategory = "Любая категория";
        public static readonly int RecordsOnPage = 5;

        protected internal readonly ICookingRecipes recipeService = default!;
        protected internal CancellationTokenSource cancellationSource = new();

        public ICommand CancelCommand { get; protected set; } = default!;
        public ICommand GetCategoriesListCommand { get; protected set; } = default!;
        public ICommand GetRecipesListCommand { get; protected set; } = default!;

        public event EventHandler CategoriesReload = delegate { };
        public event EventHandler RecipesReload = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = default;
        public RecipesListViewModel(ICookingRecipes recipeService) : base()
        {
            this.recipeService = recipeService;
            this.GetCategoriesListCommand = new Command(() =>
            {
                this.LaunchСancelableTask(() => this.GetCategoriesListCommandHandler());
            });
            this.GetRecipesListCommand = new Command(() =>
            {
                this.LaunchСancelableTask(() => this.GetRecipesListCommandHandler());
            });
            this.CancelCommand = new Command(this.CancelCommandHandler);
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            if (this.isLoading == false) return;
            this.cancellationSource.Cancel();

            this.cancellationSource = new CancellationTokenSource();
            this.IsLoading = default;
        });
        protected async void LaunchСancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
        {
            this.IsLoading = true; await cancelableTask.Invoke();
            this.IsLoading = false;
        });
        private Task<byte[]> FileToByteArray(string filename) 
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                using var binaryReader = new BinaryReader(fileStream);
                return Task.FromResult(binaryReader.ReadBytes((int)fileStream.Length));
            }
        }
        protected virtual async Task GetRecipesListCommandHandler() => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.recipeService.GetRecipesList(new RequestInfo<GetRecipesListRequestModel>()
            {
                RequestModel = new GetRecipesListRequestModel()
                {
                    Take = (RecordsOnPage * (this.PageIndex - 1)) + RecordsOnPage,
                    Skip = RecordsOnPage * (this.PageIndex - 1), 
                    SortingType = this.SortingTypeConverter.Invoke(this.SortingType),
                    TextFilter = this.TextFilter == string.Empty ? null : this.TextFilter,
                    Category = this.Category == DefaultCategory ? null : this.Category,
                },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
            foreach (var recipeRecord in requestResult.Recipes)
            {
                recipeRecord.Image = recipeRecord.Image.Length != 0
                    ? recipeRecord.Image : await this.FileToByteArray(DefaultRecipeImage);
            }
            this.CookingRecipes = new(requestResult.Recipes);
            (this.AllCount, this.PageCount) = (requestResult.AllCount, (int)Math.Ceiling(requestResult.AllCount / (double)RecordsOnPage));

            this.RecipesReload.Invoke(this, new EventArgs());
        }, (errorInfo) =>
        {
            (this.CookingRecipes, this.PageCount, this.PageIndex, this.AllCount) = (new(), 1, 1, 0);

            this.RecipesReload.Invoke(this, new EventArgs());
        });
        protected virtual async Task GetCategoriesListCommandHandler() => await UserManager.SendRequest(async (token) =>
        {
            var categoriesResult = await this.recipeService.GetCategoriesList(token, this.cancellationSource.Token);
            this.Categories = new(categoriesResult.Categories);
            this.Categories.Insert(0, DefaultCategory);

            this.CategoriesReload.Invoke(this, new EventArgs());
        }, (errorInfo) =>
        {
            this.Categories = new() { DefaultCategory };
            this.CategoriesReload.Invoke(this, new EventArgs());
        });
        public ObservableCollection<GetRecipeResponseModel> CookingRecipes { get; protected set; } = new();
        public ObservableCollection<string> Categories { get; protected set; } = new() { DefaultCategory };

        public Func<string, RecipeSortingType> SortingTypeConverter = delegate (string sortingName)
        {
            return sortingName switch
            {
                "По дате" => RecipeSortingType.ByDate, "По рейтингу" => RecipeSortingType.ByRating,
                "По названию" => RecipeSortingType.ByName, _ => RecipeSortingType.ByDate,
            };
        };
        public ObservableCollection<string> SortingTypes = new() { DefaultSortingType, "По рейтингу", "По названию" };

        private protected int pageIndex = default!;
        public int PageIndex { get => this.pageIndex; set { this.pageIndex = value; OnPropertyChanged(); } }

        private protected int pageCount = default!;
        public int PageCount { get => this.pageCount; set { this.pageCount = value; OnPropertyChanged(); } }

        private protected string textFilter = string.Empty;
        public string TextFilter { get => this.textFilter; set { this.textFilter = value; OnPropertyChanged(); } }
        public string Category { get; set; } = DefaultCategory;
        public string SortingType { get; set; } = DefaultSortingType;

        private protected int allCount = default!;
        public int AllCount { get => this.allCount; set { this.IsEmpty = (this.allCount = value) <= 0; OnPropertyChanged(); } }

        private protected bool isEmpty = default!;
        public bool IsEmpty { get => this.isEmpty; set { this.isEmpty = value; OnPropertyChanged(); } }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
