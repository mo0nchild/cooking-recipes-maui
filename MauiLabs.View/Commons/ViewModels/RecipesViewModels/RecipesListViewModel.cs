using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.RecipesViewModels
{
    public partial class RecipesListViewModel : INotifyPropertyChanged
    {
        public static readonly int RecordsOnPage = 10;

        protected internal readonly ICookingRecipes cookingRecipes = default!;
        protected internal CancellationTokenSource cancellationSource = new();

        public ICommand CancelCommand { get; protected set; } = default!;

        public event PropertyChangedEventHandler PropertyChanged = default;
        public RecipesListViewModel(ICookingRecipes recipesService) : base()
        {
            this.cookingRecipes = recipesService;
            this.CancelCommand = new Command(this.CancelCommandHandler);

            this.LaunchСancelableTask(async () => await this.LoadingRecipesList());
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            if (this.CookingRecipes != null)
            {
                this.cancellationSource.Cancel();
                this.cancellationSource = new CancellationTokenSource();
                this.IsLoading = default;
            }
        });
        protected async void LaunchСancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
        {
            this.IsLoading = true; await cancelableTask.Invoke();
            this.IsLoading = false;
        });
        protected virtual async Task LoadingRecipesList(int pageIndex = 0) => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.cookingRecipes.GetRecipesList(new RequestInfo<GetRecipesListRequestModel>()
            {
                RequestModel = new GetRecipesListRequestModel()
                {
                    Skip = RecordsOnPage * pageIndex, Take = (RecordsOnPage * pageIndex) + 10,
                    SortingType = RecipeSortingType.ByDate,
                },
                CancelToken = cancellationSource.Token, ProfileToken = token,
            });
            (this.AllCount, this.CookingRecipes) = (requestResult.AllCount, new(requestResult.Recipes));
            this.IsLoading = default;
        });
        

        public ObservableCollection<GetRecipeResponseModel> CookingRecipes { get; protected set; } = default!;

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
