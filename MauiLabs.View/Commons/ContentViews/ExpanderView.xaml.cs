using CommunityToolkit.Maui.Alerts;

namespace MauiLabs.View.Commons.ContentViews;

public partial class ExpanderView : ContentView
{
    public static readonly BindableProperty ItemProperty = BindableProperty.Create(
        "Item", typeof(Microsoft.Maui.Controls.View), typeof(ExpanderView), new StackLayout());

    public static readonly BindableProperty ButtonIconProperty = BindableProperty.Create(
        "ButtonIcon", typeof(ImageSource), typeof(RatingStarView), null);

    public ImageSource ButtonIcon
    {
        get => (ImageSource)this.GetValue(ButtonIconProperty); set => this.SetValue(ButtonIconProperty, value);
    }
    public Microsoft.Maui.Controls.View Item 
    { 
        get => (Microsoft.Maui.Controls.View)this.GetValue(ItemProperty); set => this.SetValue(ItemProperty, value); 
    }
    public virtual int ExpandTapsRequired { get => 2; }
    public ExpanderView() : base()
	{
		this.InitializeComponent();

        var buttonTapRecognizer = new TapGestureRecognizer() { NumberOfTapsRequired = this.ExpandTapsRequired };
        buttonTapRecognizer.Tapped += this.ExpandButtonTapHandler;

        this.ExpandButton.GestureRecognizers.Add(buttonTapRecognizer);
	}

    protected virtual void ExpandButtonTapHandler(object sender, TappedEventArgs args)
    {
        
    }

    protected override void InvalidateLayout() => base.InvalidateLayout();
}