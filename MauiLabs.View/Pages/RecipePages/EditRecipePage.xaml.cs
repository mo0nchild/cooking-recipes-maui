using MauiLabs.View.Services.Interfaces;
using System.ComponentModel;
using System.Reflection;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MauiLabs.View.Services.ApiModels.Commons.RecipeModels;
using MauiLabs.View.Services.Commons;
using Microsoft.Maui.Dispatching;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;

namespace MauiLabs.View.Pages.RecipePages;

public partial class EditRecipePage : ContentPage, INotifyPropertyChanged, INavigationService.IQueryableNavigation
{
    protected internal CancellationTokenSource cancellationSource = new();
    protected internal readonly ICookingRecipes cookingRecipes = default!;

    public static readonly string DefaultRecipeImage = $"MauiLabs.View.Resources.Images.Recipe.defaultrecipe.jpg";
    public virtual double ImageSize { get => 96; }

    public ICommand EditRecipeCommand { get; protected set; } = default!;
    public ICommand DeleteIngredientCommand { get; protected set; } = default!;
    public ICommand CancelCommand { get; protected set; } = default!;

    public EditRecipePage(ICookingRecipes cookingRecipes) : base()
	{
		this.InitializeComponent();
        this.cookingRecipes = cookingRecipes;

        this.DeleteIngredientCommand = new Command<string>(this.DeleteIngredientCommandHandler);
        this.EditRecipeCommand = new Command(() => 
        {
            this.LaunchСancelableTask(() => this.EditRecipeCommandHandler());
        });
        this.CancelCommand = new Command(this.CancelCommandHandler);
    }
    private protected bool isLoading = default;
    public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

    private protected string recipeName = default!;
    public string RecipeName { get => this.recipeName; set { this.recipeName = value; OnPropertyChanged(); } }

    private protected string description = default!;
    public string Description { get => this.description; set { this.description = value; OnPropertyChanged(); } }

    private protected byte[] recipeImage = new byte[0];
    public byte[] RecipeImage { get => this.recipeImage; set { this.recipeImage = value; OnPropertyChanged(); } }

    public sealed class IngredientModel : object
    {
        public required string Name { get; set; } = default!;
        public required string Unit { get; set; } = default!;
        public required string Value { get; set; } = default!;
    }
    private protected bool ingredientsEmpty = true;
    public bool IngredientsEmpty { get => this.ingredientsEmpty; set { this.ingredientsEmpty = value; OnPropertyChanged(); } }

    public virtual ObservableCollection<string> Categories { get; protected set; } = new();
    public virtual ObservableCollection<string> Units { get; protected set; } = new();
    public virtual ObservableCollection<IngredientModel> Ingredients { get; protected set; } = new();

    public Dictionary<string, bool> ValidationState { get; set; } = new();
    public virtual int RecipeId { get; protected set; } = default!;

    protected async void LaunchСancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
    {
        this.IsLoading = true; await cancelableTask.Invoke();
        this.IsLoading = false;
    });
    protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
    {
        if (this.isLoading == false) return;

        this.cancellationSource.Cancel();
        this.cancellationSource = new CancellationTokenSource();

        (this.IsLoading) = (default);
    });
    public virtual byte[] FileToByteArray(string filename)
    {
        using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
        {
            using var binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
    protected virtual void DisplayMessage(string title, string message)
    {
        this.Dispatcher.Dispatch(async () => await this.DisplayAlert(title, message, "Назад"));
    }
    protected virtual void ReloadProfileImage(byte[] image) => this.Dispatcher.Dispatch(() =>
    {
        this.RecipeImageContent.Content = new Image()
        {
            Source = ImageSource.FromStream(() => new MemoryStream(image)),
            Margin = Thickness.Zero, Aspect = Aspect.AspectFill,
        };
    });

    protected virtual async void ImagePickerButton_Clicked(object sender, EventArgs args)
    {
        var fileFilter = (FileResult result, string extension) =>
        {
            return result.FileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
        };
        var pickerOption = new PickOptions() { FileTypes = FilePickerFileType.Images, PickerTitle = "Выберите изображение" };
        try
        {
            var pickerResult = await FilePicker.Default.PickAsync(pickerOption);
            if (pickerResult != null && (fileFilter(pickerResult, "jpg") || fileFilter(pickerResult, "png")))
            {
                using var stream = await pickerResult.OpenReadAsync();
                using var image = SixLabors.ImageSharp.Image.Load(stream);
                image.Mutate(prop => prop.Resize((int)this.ImageSize, (int)this.ImageSize, false));

                using var outputStream = new MemoryStream();
                image.Save(outputStream, new PngEncoder());
                this.ReloadProfileImage(this.RecipeImage = outputStream.ToArray());
            }
        }
        catch (Exception errorInfo) { this.DisplayMessage("Произошла ошибка", errorInfo.Message); }
    }
    protected virtual void ImageClearButton_Clicked(object sender, EventArgs args)
    {
        this.RecipeImage = null;
        this.ReloadProfileImage(this.FileToByteArray(DefaultRecipeImage));
    }
    protected virtual void UnitValueSlider_ValueChanged(object sender, ValueChangedEventArgs args)
    {
        this.Dispatcher.Dispatch(() => this.UnitValueTextField.Text = $"{Math.Round(args.NewValue / 10.0):F2}");
    }
    protected virtual void AddIngredientButton_Clicked(object sender, EventArgs args) => this.Dispatcher.Dispatch(async () =>
    {
        if (this.ValidationState["IngredientName"])
        {
            if (this.Ingredients.Count <= 0) this.IngredientsEmpty = default;
            this.Ingredients.Add(new IngredientModel()
            {
                Name = this.IngredientNameTextField.TextValue, Value = $"{Math.Round(this.UnitValueSlider.Value / 100.0):F2}",
                Unit = this.Units[this.UnitsPicker.SelectedIndex],
            });
            this.IngredientsView.ItemsSource = this.Ingredients;

            this.UnitValueSlider.Value = default!;
            this.IngredientNameTextField.TextValue = string.Empty;
        }
        else await this.DisplayAlert("Произошла ошибка", "Неверное название ингредиента", "Назад");
    });
    private void IngredientsView_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        this.DeleteIngredientCommand.Execute(((IngredientModel)this.IngredientsView.SelectedItem).Name);
    }

    protected virtual void DeleteIngredientCommandHandler(string name) => this.Dispatcher.Dispatch(() =>
    {
        this.IngredientsView.ItemsSource = this.Ingredients = new(this.Ingredients.Where(item => item.Name != name));
        if (this.Ingredients.Count <= 0) this.IngredientsEmpty = true;
    });
    protected virtual void EditRecipeButton_Clicked(object sender, EventArgs args) => this.EditRecipeCommand.Execute(this);
    protected virtual async Task EditRecipeCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        if(this.ValidationState["RecipeName"] && this.Ingredients.Count > 0)
        {
            if (this.RecipeId <= 0) await this.cookingRecipes.AddRecipeInfo(new RequestInfo<AddRecipeRequestModel>() 
            { 
                RequestModel = new AddRecipeRequestModel() 
                { 
                    Name = this.RecipeName, Description = this.Description, Image = this.RecipeImage,
                    Ingredients = this.Ingredients.ToDictionary(k => k.Name, v => new IngredientUnitModel() 
                    { 
                        Unit = v.Unit, Value = double.Parse(v.Value) 
                    }),
                    Category = this.Categories[this.CategoriesPicker.SelectedIndex],
                },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
            else await this.cookingRecipes.EditRecipeInfo(new RequestInfo<EditRecipeRequestModel>()
            {
                RequestModel = new EditRecipeRequestModel()
                {
                    Id = this.RecipeId, Name = this.RecipeName, Description = this.Description, Image = this.RecipeImage,
                    Ingredients = this.Ingredients.ToDictionary(k => k.Name, v => new IngredientUnitModel()
                    {
                        Unit = v.Unit, Value = double.Parse(v.Value)
                    }),
                    Category = this.Categories[this.CategoriesPicker.SelectedIndex],
                },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
            this.Dispatcher.Dispatch(async () => await Shell.Current.Navigation.PopAsync(animated: true));
        }
        else await this.Dispatcher.DispatchAsync(async () =>
        {
            await this.DisplayAlert("Произошла ошибка", "Неверное название рецепта или пустой список ингредиентов", "Назад");
        });
    }, async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true));
    protected virtual async Task GetRecipeInfoCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        var categoriesResult = await this.cookingRecipes.GetCategoriesList(token, this.cancellationSource.Token);
        await this.Dispatcher.DispatchAsync(() =>
        {
            this.CategoriesPicker.ItemsSource = this.Categories = new(categoriesResult.Categories);
            if (this.CategoriesPicker.Items.Count > 0) this.CategoriesPicker.SelectedIndex = 0;
        });
        var unitsResult = await this.cookingRecipes.GetUnitsList(token, this.cancellationSource.Token);
        await this.Dispatcher.DispatchAsync(() =>
        {
            this.UnitsPicker.ItemsSource = this.Units = new(unitsResult.IngredientUnits.Select(p => p.Name));
            if (this.UnitsPicker.Items.Count > 0) this.UnitsPicker.SelectedIndex = 0;
        });
        if (this.RecipeId <= 0) return;
        var recipeResult = await this.cookingRecipes.GetRecipeInfo(new RequestInfo<GetRecipeRequestModel>()
        {
            RequestModel = new GetRecipeRequestModel() { RecipeId = this.RecipeId },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        await this.Dispatcher.DispatchAsync(() =>
        {
            if (recipeResult.Image.Length <= 0)
            {
                this.ReloadProfileImage(this.FileToByteArray(DefaultRecipeImage));
                this.RecipeImage = null;
            }
            else this.ReloadProfileImage(this.RecipeImage = recipeResult.Image);
            (this.RecipeName, this.Description) = (recipeResult.Name, recipeResult.Description);
            if (recipeResult.Ingredients.Count > 0)
            {
                this.Ingredients = new(recipeResult.Ingredients.Select(item => new IngredientModel()
                {
                    Name = item.Name, Unit = item.Unit, Value = item.Value.ToString()
                }));
                (this.IngredientsView.ItemsSource, this.IngredientsEmpty) = (this.Ingredients, default!);
            }
            this.CategoriesPicker.SelectedIndex = this.Categories.IndexOf(recipeResult.Category);
        });
    }, async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true));
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.ReloadProfileImage(this.FileToByteArray(DefaultRecipeImage));
        this.LaunchСancelableTask(() => this.GetRecipeInfoCommandHandler());
        await Task.WhenAll(new Task[]
        {
            this.RecipePanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.RecipePanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.RecipePanel.Opacity, this.RecipePanel.Scale) = (1.0, 1.0);
    });
    public virtual void SetNavigationQuery(IDictionary<string, object> queries) => this.RecipeId = (int)queries["RecipeId"];
    protected override void OnDisappearing() => base.OnDisappearing();
}