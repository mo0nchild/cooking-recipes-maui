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
        public static readonly int RecordsOnPage = 10;

        protected internal readonly ICookingRecipes recipeService = default!;
        protected internal CancellationTokenSource cancellationSource = new();

        public ICommand CancelCommand { get; protected set; } = default!;
        public ICommand LoadRecipesListCommand { get; protected set; } = default!;

        public event EventHandler RecipesReloaded = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = default;
        public RecipesListViewModel(ICookingRecipes recipeService) : base()
        {
            this.CancelCommand = new Command(this.CancelCommandHandler);
            this.LoadRecipesListCommand = new Command(() =>
            {
                this.LaunchСancelableTask(() => this.LoadingRecipesList());
            });
            this.recipeService = recipeService;
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
        protected virtual async Task LoadingRecipesList(int pageIndex = 0) => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.recipeService.GetRecipesList(new RequestInfo<GetRecipesListRequestModel>()
            {
                RequestModel = new GetRecipesListRequestModel()
                {
                    Skip = RecordsOnPage * pageIndex, Take = (RecordsOnPage * pageIndex) + 10,
                    SortingType = RecipeSortingType.ByDate,
                },
                CancelToken = cancellationSource.Token, ProfileToken = token,
            });
            foreach (var recipeRecord in requestResult.Recipes)
            {
                recipeRecord.Image = recipeRecord.Image.Length != 0 
                    ? recipeRecord.Image : await this.FileToByteArray(DefaultRecipeImage);
            }
            this.CookingRecipes = new(requestResult.Recipes);
            this.AllCount = requestResult.AllCount;

            this.RecipesReloaded.Invoke(this, new EventArgs());
        });
        public ObservableCollection<GetRecipeResponseModel> CookingRecipes { get; protected set; } = new();

        private protected int allCount = default!;
        public int AllCount { get => this.allCount; set { this.allCount = value; OnPropertyChanged(); } }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
