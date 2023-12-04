namespace MauiLabs.View.Commons.ContentViews;

public partial class PaginationView : ContentView
{
    public static readonly BindableProperty ButtonIconProperty = BindableProperty.Create(
        "ButtonIcon", typeof(ImageSource), typeof(PaginationView), null);

    public static readonly BindableProperty ItemSizeProperty = BindableProperty.Create(
        "ItemSize", typeof(double), typeof(PaginationView), null);

    public double ItemSize { get => (double)this.GetValue(ItemSizeProperty); set => this.SetValue(ItemSizeProperty, value); }
    public ImageSource ButtonIcon 
    {
        get => (ImageSource)this.GetValue(ButtonIconProperty); set => this.SetValue(ButtonIconProperty, value); 
    }
    public PaginationView() : base()
	{
		this.InitializeComponent();
	}
    protected override void InvalidateLayout() => base.InvalidateLayout();
}