using System.Windows.Input;

namespace MauiLabs.View.Commons.ContentViews;

public partial class LoadingContentView : ContentView
{
    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
            "IsLoading", typeof(bool),
            typeof(LoadingContentView), false, propertyChanged: LoadingPropertyChanged);

    public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            "CancelCommand", typeof(ICommand), typeof(LoadingContentView));

    public bool IsLoading { get => (bool)this.GetValue(IsLoadingProperty); set => this.SetValue(IsLoadingProperty, value); }
    public ICommand CancelCommand 
    { 
        get => (ICommand)this.GetValue(CancelCommandProperty); set => this.SetValue(CancelCommandProperty, value); 
    }
    public LoadingContentView() : base() => this.InitializeComponent();

    protected static async void LoadingPropertyChanged(BindableObject @object, object oldValue, object newValue)
    {
        if(@object is LoadingContentView loadingAnimator)
        {
            if (loadingAnimator.IsLoading) { loadingAnimator.IsVisible = true; await loadingAnimator.PlayAnimation(); }
            else { await loadingAnimator.StopAnimation(); loadingAnimator.IsVisible = false; }
        }
    }
    protected virtual async Task PlayAnimation() => await Task.WhenAll(new Task[]
    {
        this.LoadingPanel.ScaleTo(1.0, length: 800, easing: Easing.SinInOut),
        this.FadeTo(1.0, length: 800, easing: Easing.SinInOut),
    });
    protected virtual async Task StopAnimation() => await Task.WhenAll(new Task[]
    {
        this.LoadingPanel.ScaleTo(0.0, length: 800, easing: Easing.SinInOut),
        this.FadeTo(0.0, length: 800, easing: Easing.SinInOut),
    });
}