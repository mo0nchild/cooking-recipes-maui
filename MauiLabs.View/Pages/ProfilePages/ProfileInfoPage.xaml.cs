using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class ProfileInfoPage : ContentPage
{
    protected internal readonly ProfileInfoViewModel viewModel = default!;
    public ProfileInfoPage(ProfileInfoViewModel viewModel) : base()
	{
		this.InitializeComponent();
        this.BindingContext = this.viewModel = viewModel;
        
        this.viewModel.DisplayAlert += delegate (object sender, string message)
        {
            this.Dispatcher.Dispatch(async() => await this.DisplayAlert("Произошла ошибка", message, "Назад"));
        };
        this.viewModel.DisplayInfo += delegate (object sender, string message)
        {
            this.Dispatcher.Dispatch(async () => await this.DisplayAlert("Успешное действие", message, "Назад"));
        };
        this.viewModel.ReloadImage += (sender, image) => this.Dispatcher.Dispatch(() =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    Shell.Current.Dispatcher.Dispatch(() =>
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            this.Dispatcher.Dispatch(() =>
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    this.Dispatcher.Dispatch(() =>
                                    {
                                        Application.Current.Dispatcher.Dispatch(() =>
                                        {
                                            MainThread.BeginInvokeOnMainThread(() =>
                                            {
                                                Application.Current.Dispatcher.Dispatch(() =>
                                                {
                                                    Shell.Current.Dispatcher.Dispatch(() =>
                                                    {
                                                        MainThread.BeginInvokeOnMainThread(() =>
                                                        {
                                                            this.Dispatcher.Dispatch(() =>
                                                            {
                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                {
                                                                    this.Dispatcher.Dispatch(() =>
                                                                    {
                                                                        Application.Current.Dispatcher.Dispatch(() =>
                                                                        {
                                                                            Shell.Current.Dispatcher.Dispatch(() =>
                                                                            {
                                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                                {
                                                                                    Application.Current.Dispatcher.Dispatch(() =>
                                                                                    {
                                                                                        Shell.Current.Dispatcher.Dispatch(() =>
                                                                                        {
                                                                                            MainThread.BeginInvokeOnMainThread(() =>
                                                                                            {
                                                                                                this.Dispatcher.Dispatch(() =>
                                                                                                {
                                                                                                    MainThread.BeginInvokeOnMainThread(() =>
                                                                                                    {
                                                                                                        this.Dispatcher.Dispatch(() =>
                                                                                                        {
                                                                                                            Application.Current.Dispatcher.Dispatch(() =>
                                                                                                            {
                                                                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                {
                                                                                                                    Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                    {
                                                                                                                        Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                        {
                                                                                                                            MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                            {
                                                                                                                                this.Dispatcher.Dispatch(() =>
                                                                                                                                {
                                                                                                                                    MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                    {
                                                                                                                                        this.Dispatcher.Dispatch(() =>
                                                                                                                                        {
                                                                                                                                            Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                            {
                                                                                                                                                Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                {
                                                                                                                                                    MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                    {
                                                                                                                                                        Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                        {
                                                                                                                                                            Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                            {
                                                                                                                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                {
                                                                                                                                                                    this.Dispatcher.Dispatch(() =>
                                                                                                                                                                    {
                                                                                                                                                                        MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                        {
                                                                                                                                                                            this.Dispatcher.Dispatch(() =>
                                                                                                                                                                            {
                                                                                                                                                                                Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                {
                                                                                                                                                                                    MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                    {
                                                                                                                                                                                        Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                        {
                                                                                                                                                                                            Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                            {
                                                                                                                                                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                {
                                                                                                                                                                                                    this.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                    {
                                                                                                                                                                                                        MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                        {
                                                                                                                                                                                                            this.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    this.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            this.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    this.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        MainThread.BeginInvokeOnMainThread(() =>
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            this.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                Application.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                    Shell.Current.Dispatcher.Dispatch(() =>
                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                        this.ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(image));
                                                                                                                                                                                                                                                                                    });
                                                                                                                                                                                                                                                                                });
                                                                                                                                                                                                                                                                            });
                                                                                                                                                                                                                                                                        });
                                                                                                                                                                                                                                                                    });
                                                                                                                                                                                                                                                                });

                                                                                                                                                                                                                                                            });
                                                                                                                                                                                                                                                        });
                                                                                                                                                                                                                                                    });
                                                                                                                                                                                                                                                });
                                                                                                                                                                                                                                            });
                                                                                                                                                                                                                                        });
                                                                                                                                                                                                                                    });
                                                                                                                                                                                                                                });

                                                                                                                                                                                                                            });
                                                                                                                                                                                                                        });
                                                                                                                                                                                                                    });
                                                                                                                                                                                                                });
                                                                                                                                                                                                            });
                                                                                                                                                                                                            });
                                                                                                                                                                                                        });
                                                                                                                                                                                                    });
                                                                                                                                                                                                });

                                                                                                                                                                                            });
                                                                                                                                                                                        });
                                                                                                                                                                                    });
                                                                                                                                                                                });
                                                                                                                                                                            });
                                                                                                                                                                        });
                                                                                                                                                                    });
                                                                                                                                                                });

                                                                                                                                                            });
                                                                                                                                                        });
                                                                                                                                                    });
                                                                                                                                                });
                                                                                                                                            });
                                                                                                                                            });
                                                                                                                                        });
                                                                                                                                    });
                                                                                                                                });
                                                                                                                            });

                                                                                                                        });
                                                                                                                    });
                                                                                                                });
                                                                                                            });
                                                                                                        });
                                                                                                    });
                                                                                                });
                                                                                            });

                                                                                        });
                                                                                    });
                                                                                });
                                                                            });
                                                                        });
                                                                        });
                                                                    });
                                                                });
                                                            });
                                                        });

                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                            });
                        });

                    });
                });
            });
        });
    }

    protected override void OnAppearing() => this.Dispatcher.Dispatch(() =>
    {
        this.viewModel.GetProfileCommand.Execute(this);
    });
    protected override void OnDisappearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);
        await this.PageScroller.ScrollToAsync(0, 0, false);
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
    protected virtual void UpdateButton_Clicked(object sender, EventArgs args) { }
    protected virtual async void ReferenceLinkButton_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = string.Format("Используйте ссылку в приложение: {0}\n", this.viewModel.ReferenceLink),
            Title = "Ссылка для добавление в друзья",
            Uri = @"https://github.com/mo0nchild/cs-maui-labs",
        });
    }
}