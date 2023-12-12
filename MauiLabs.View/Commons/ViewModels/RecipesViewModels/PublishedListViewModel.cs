using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.RecipesViewModels
{
    public partial class PublishedListViewModel : INotifyPropertyChanged
    {
        protected internal CancellationTokenSource cancellationSource = new();
        protected internal readonly ICookingRecipes cookingRecipes = default!;

        public static readonly string DefaultRecipeImage = $"MauiLabs.View.Resources.Images.Recipe.defaultrecipe.jpg";

        public ICommand GetPublishedListCommand { get; protected set; } = default!;
        public ICommand DeletePublishedCommand { get; protected set; } = default!;
        public ICommand CancelCommand { get; protected set; } = default!;

        public event EventHandler<string> DisplayInfo = delegate { };
        public event EventHandler RecipesReload = delegate { };
        public event Func<string, Task<bool>> CheckСonfirm = (_) => Task.FromResult(false);  

        public event PropertyChangedEventHandler PropertyChanged;
        public PublishedListViewModel(ICookingRecipes cookingRecipes) : base() 
        {
            this.cookingRecipes = cookingRecipes;
            this.GetPublishedListCommand = new Command(() =>
            {
                this.LaunchСancelableTask(() => this.GetPublishedListCommandHandler());
            });
            this.DeletePublishedCommand = new Command<int>(async (id) =>
            {
                if (await this.CheckСonfirm.Invoke("Удалить данный рецепт?"))
                {
                    this.LaunchСancelableTask(() => this.DeletePublishedCommandHandler(id));
                }
            });
            this.CancelCommand = new Command(this.CancelCommandHandler);
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            if (this.isLoading == false) return;
            this.cancellationSource.Cancel();

            this.cancellationSource = new CancellationTokenSource();
            (this.IsLoading, this.RecipesLoaded, this.AllCount) = (default, default, 0);
        });
        protected async void LaunchСancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
        {
            this.IsLoading = true; await cancelableTask.Invoke();
            this.IsLoading = false;
        });
        public virtual byte[] FileToByteArray(string filename)
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                using var binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        protected virtual async Task GetPublishedListCommandHandler() => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.cookingRecipes.GetPublishedList(token, this.cancellationSource.Token);
            foreach (var friendRecord in requestResult.Recipes)
            {
                friendRecord.Image = friendRecord.Image.Length != 0
                    ? friendRecord.Image : this.FileToByteArray(DefaultRecipeImage);
            }
            this.CookingRecipes = new(requestResult.Recipes);
            this.AllCount = requestResult.AllCount;

            this.RecipesReload.Invoke(this, new EventArgs());
            if (!this.RecipesLoaded) this.RecipesLoaded = true;
        });
        protected virtual async Task DeletePublishedCommandHandler(int id) => await UserManager.SendRequest(async (token) =>
        {
            await this.cookingRecipes.DeleteRecipeInfo(new RequestInfo<DeleteRecipeRequestModel>() 
            {
                RequestModel = new DeleteRecipeRequestModel() { Id = id },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
            await this.GetPublishedListCommandHandler();
            this.DisplayInfo.Invoke(this, "Рецепт удален");
        });
        public ObservableCollection<GetRecipeResponseModel> CookingRecipes { get; protected set; } = new();

        private protected int allCount = default!;
        public int AllCount { get => this.allCount; set { this.IsEmpty = (this.allCount = value) <= 0; OnPropertyChanged(); } }

        private protected bool isEmpty = default!;
        public bool IsEmpty { get => this.isEmpty; set { this.isEmpty = value; OnPropertyChanged(); } }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        private protected bool recipesLoaded = default;
        public bool RecipesLoaded { get => this.recipesLoaded; set { this.recipesLoaded = value; OnPropertyChanged(); } }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
