using System.Runtime.CompilerServices;

namespace MauiLabs.View.Commons.ContentViews;

public partial class RatingStarView : ContentView
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
           "Value", typeof(int),
           typeof(RatingStarView), default(int), propertyChanged: ValuePropertyChangedHandler);
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
           "MaxValue", typeof(int), typeof(RatingStarView), default(int));

    public static readonly BindableProperty IconHeightProperty = BindableProperty.Create(
           "IconHeight", typeof(double), typeof(RatingStarView), defaultValueCreator: (@object) => DefaultIconSize);
    public static readonly BindableProperty IconWidthProperty = BindableProperty.Create(
           "IconWidth", typeof(double), typeof(RatingStarView), defaultValueCreator: (@object) => DefaultIconSize);

    public static readonly BindableProperty NullIconProperty = BindableProperty.Create(
           "NullIcon", typeof(ImageSource), typeof(RatingStarView), null);
    public static readonly BindableProperty ValueIconProperty = BindableProperty.Create(
           "ValueIcon", typeof(ImageSource), typeof(RatingStarView), null);

    public int MaxValue { get => (int)this.GetValue(MaxValueProperty); set => this.SetValue(MaxValueProperty, value); }
    public int Value { get => (int)this.GetValue(ValueProperty); set => this.SetValue(ValueProperty, value); }

    protected internal static readonly double DefaultIconSize = 16.0;
    public double IconHeight { get => (double)this.GetValue(IconHeightProperty); set => this.SetValue(IconHeightProperty, value); }
    public double IconWidth { get => (double)this.GetValue(IconWidthProperty); set => this.SetValue(IconWidthProperty, value); }

    public ImageSource NullIcon
    {
        get => (ImageSource)this.GetValue(NullIconProperty); set => this.SetValue(NullIconProperty, value);
    }
    public ImageSource ValueIcon
    {
        get => (ImageSource)this.GetValue(ValueIconProperty); set => this.SetValue(ValueIconProperty, value);
    }

    protected static void ValuePropertyChangedHandler(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RatingStarView ratingView)
        {
            ratingView.RatingContainer.Children.Clear();
            for (var index = 0; index < ratingView.MaxValue; index++)
            {
                var ratingIconImage = new Image()
                {
                    HeightRequest = ratingView.IconHeight,
                    WidthRequest = ratingView.IconWidth
                };
                if (index < ratingView.Value) ratingIconImage.Source = ratingView.ValueIcon;
                else ratingIconImage.Source = ratingView.NullIcon;
                ratingView.RatingContainer.Add(ratingIconImage);
            }
            ratingView.Dispatcher.Dispatch(() => ratingView.InvalidateLayout());
        }
    }
    public RatingStarView() : base()
    {
        this.InitializeComponent();
        RatingStarView.ValuePropertyChangedHandler(this, default(int), Value);
    }
    protected override void InvalidateLayout() => base.InvalidateLayout();
}