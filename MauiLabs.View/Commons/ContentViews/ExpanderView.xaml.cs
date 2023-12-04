using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace MauiLabs.View.Commons.ContentViews;

using MauiView = Microsoft.Maui.Controls.View;

public partial class ExpanderView : ContentView
{
    public static readonly BindableProperty ItemProperty = BindableProperty.Create(
        "Item", typeof(MauiView), typeof(ExpanderView), new StackLayout());

    public static readonly BindableProperty CanExpandProperty = BindableProperty.Create(
        "CanExpand", typeof(bool), typeof(ValidationEntryView), defaultValue: true);

    public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(
        "ButtonText", typeof(string), 
        typeof(ExpanderView), defaultValueCreator: (@object) => "Раскрыть");

    public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(
        "IsExpanded", typeof(bool), typeof(ExpanderView), default!);

    public static readonly BindableProperty ButtonIconProperty = BindableProperty.Create(
        "ButtonIcon", typeof(ImageSource), typeof(ExpanderView), null);

    public static readonly BindableProperty ButtonHeightProperty = BindableProperty.Create(
        "ButtonHeight", typeof(double), typeof(ExpanderView), defaultValue: 48.0);

    public static readonly BindableProperty ExpanderHeightProperty = BindableProperty.Create(
        "ExpanderHeight", typeof(double), typeof(ExpanderView), default!);

    public MauiView Item { get => (MauiView)this.GetValue(ItemProperty); set => this.SetValue(ItemProperty, value); }
    public string ButtonText { get => (string)this.GetValue(ButtonTextProperty); set => this.SetValue(ButtonTextProperty, value); }
    public double ButtonHeight 
    { 
        get => (double)this.GetValue(ButtonHeightProperty); set => this.SetValue(ButtonHeightProperty, value); 
    }
    public ImageSource ButtonIcon
    {
        get => (ImageSource)this.GetValue(ButtonIconProperty); set => this.SetValue(ButtonIconProperty, value);
    }
    public double ExpanderHeight { get => (double)GetValue(ExpanderHeightProperty); set => SetValue(ExpanderHeightProperty, value); }
    public bool CanExpand { get => (bool)GetValue(CanExpandProperty); set => SetValue(CanExpandProperty, value); }
    public bool IsExpanded { get => (bool)GetValue(IsExpandedProperty); set => SetValue(IsExpandedProperty, value); }

    public virtual int ExpandTapsRequired { get => 1; }
    public virtual bool IsPlaying { get; protected set; } = default!;

    protected Animation CompressAnimation { get => new(prop => this.MainContent.HeightRequest = prop, 
        this.ExpanderHeight, 0, easing: Easing.Linear); }
    protected Animation ExpandAnimation { get => new(prop => this.MainContent.HeightRequest = prop, 
        0, this.ExpanderHeight, easing: Easing.Linear); }
    public ExpanderView() : base()
    {
		this.InitializeComponent();
        var buttonTapRecognizer = new TapGestureRecognizer() { NumberOfTapsRequired = this.ExpandTapsRequired };
        buttonTapRecognizer.Tapped += this.ExpandButtonTapHandler;

        this.Dispatcher.Dispatch(() =>
        {
            this.MainContent.Padding = new Thickness(0, this.ExpandButton.Height, 0, 0);
            this.Item.Opacity = 0;
        });
        this.ExpandButton.GestureRecognizers.Add(buttonTapRecognizer);
	}
    protected virtual async void ExpandButtonTapHandler(object sender, TappedEventArgs args)
    {
        var currentAnimation = this.IsExpanded ? this.CompressAnimation : this.ExpandAnimation;
        if (this.IsPlaying == true || !this.CanExpand) return; else this.IsPlaying = true;

        if (this.IsExpanded) await this.Item.FadeTo(0.0, 500, easing: Easing.SinInOut);
        await Task.WhenAll(new Task[]
        {
            this.ExpanderIcon.RelRotateTo(180, length: 500, easing: Easing.Linear),
            this.Dispatcher.DispatchAsync(() => this.MainContent.Animate("Expand", currentAnimation, length: 600)),
        });
        if (!this.IsExpanded) await this.Item.FadeTo(1.0, 500, easing: Easing.SinInOut);
        (this.IsPlaying, this.IsExpanded) = (default!, !this.IsExpanded);
    }
    public virtual void ResetExpander()
    {
        (this.ExpanderIcon.Rotation, this.MainContent.HeightRequest, this.Item.Opacity) = (default, default, default);
        this.SetValue(IsExpandedProperty, false);
    }
    protected override void InvalidateLayout() => base.InvalidateLayout();
}