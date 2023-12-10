using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class ProfileInfoPage : ContentPage
{
    protected internal readonly ProfileInfoViewModel viewModel = default!;
    public virtual double ImageSize { get => 96; }

    protected internal bool isPageLoaded = default!;
    public ProfileInfoPage(ProfileInfoViewModel viewModel) : base()
	{
		this.InitializeComponent();
        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
        this.BindingContext = this.viewModel = viewModel;

        this.viewModel.DisplayAlert += (sender, message) => this.DisplayMessage("Произошла ошибка", message);
        this.viewModel.DisplayInfo += (sender, message) => this.DisplayMessage("Успешное действие", message);

        this.viewModel.ReloadImage += (sender, image) => this.ReloadProfileImage(image);
        this.viewModel.CheckСonfirm += (message) => this.DisplayAlert("Подтверждение", message, "Ок", "Назад");
    }
    protected virtual void DisplayMessage(string title, string message)
    {
        this.Dispatcher.Dispatch(async () => await this.DisplayAlert(title, message, "Назад"));
    }
    protected virtual void ReloadProfileImage(byte[] image) => this.Dispatcher.Dispatch(() =>
    {
        this.ProfileImage.Content = new Image()
        {
            Source = ImageSource.FromStream(() => new MemoryStream(image)),
            Margin = Thickness.Zero, Aspect = Aspect.AspectFill,
        };
    });
    protected virtual async void ExitProfile_Clicked(object sender, EventArgs args)
    {
        await UserManager.LogoutUser();
        await Application.Current!.Dispatcher.DispatchAsync(async () =>
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync(UserManager.AuthorizationRoute);
        });
    }
    protected virtual async void ImagePickerButton_Clicked(object sender, EventArgs args)
    {
        if (!this.viewModel.ProfileLoaded) { this.DisplayMessage("Произошла ошибка", "Необходимо перезагрузить профиль"); return; }
        var fileFilter = (FileResult result, string extension) =>
        {
            return result.FileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
        };
        var pickerOption = new PickOptions() { FileTypes = FilePickerFileType.Images, PickerTitle = "Выберите изображение" };
        try {
            var pickerResult = await FilePicker.Default.PickAsync(pickerOption);
            if (pickerResult != null && (fileFilter(pickerResult, "jpg") || fileFilter(pickerResult, "png")))
            {
                using var stream = await pickerResult.OpenReadAsync();
                using var image = SixLabors.ImageSharp.Image.Load(stream);
                image.Mutate(prop => prop.Resize((int)this.ImageSize, (int)this.ImageSize, false));

                using var outputStream = new MemoryStream();
                image.Save(outputStream, new PngEncoder());
                this.ReloadProfileImage(this.viewModel.UserImage = outputStream.ToArray());
            }
        }
        catch (Exception errorInfo) { this.DisplayMessage("Произошла ошибка", errorInfo.Message); }
    }
    protected virtual void ImageClearButton_Clicked(object sender, EventArgs args)
    {
        if (!this.viewModel.ProfileLoaded) { this.DisplayMessage("Произошла ошибка", "Необходимо перезагрузить профиль"); return; }
        this.viewModel.UserImage = null;
        this.ReloadProfileImage(this.viewModel.FileToByteArray(ProfileInfoViewModel.DefaultProfileImage));
    }
    protected virtual async void ReferenceLinkButton_Clicked(object sender, EventArgs args)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = string.Format("Используйте ссылку в приложение: {0}\n", this.viewModel.ReferenceLink),
            Title = "Ссылка для добавление в друзья",
            Uri = @"https://github.com/mo0nchild/cs-maui-labs",
        });
    }

    protected override void OnAppearing() => this.Dispatcher.Dispatch(async() =>
    {
        this.ReloadProfileImage(this.viewModel.FileToByteArray(ProfileInfoViewModel.DefaultProfileImage));
        this.viewModel.GetProfileCommand.Execute(this);
        await Task.Run(() => { while (!this.isPageLoaded) ; });
        await Task.WhenAll(new Task[]
        {
            this.ProfilePanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.ProfilePanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.ProfilePanel.Opacity, this.ProfilePanel.Scale) = (1.0, 1.0);
    });
    protected override void OnDisappearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);
        (this.ProfilePanel.Opacity, this.ProfilePanel.Scale) = (0, 1.5);

        this.EmailTextField.TextValue = this.SurnameTextField.TextValue = this.NameTextField.TextValue = "Не загружено";
        this.PasswordExpander.ResetExpander();
        this.OldPasswordTextField.TextValue = this.NewPasswordTextField.TextValue = string.Empty;

        this.ReloadProfileImage(this.viewModel.FileToByteArray(ProfileInfoViewModel.DefaultProfileImage));
        this.viewModel.UserImage = null;
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
}