using CommunityToolkit.Maui.Behaviors;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ContentViews
{
#nullable enable
    public partial class ValidationEntryView : ContentView, INotifyPropertyChanged
    {
        public static readonly BindableProperty IsValidatedProperty = BindableProperty.Create(
            nameof(IsValidated), typeof(bool), typeof(ValidationEntryView),
            defaultValue: false, defaultBindingMode: BindingMode.OneWayToSource);

        public static readonly BindableProperty TextValueProperty = BindableProperty.Create(
            nameof(TextValue), typeof(string), typeof(ValidationEntryView),
            defaultValue: string.Empty, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
            nameof(ErrorText), typeof(string), typeof(ValidationEntryView));

        public static readonly BindableProperty DefaultTextProperty = BindableProperty.Create(
            nameof(DefaultText), typeof(string), typeof(ValidationEntryView));

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
            nameof(LabelText), typeof(string), typeof(ValidationEntryView));

        public static readonly BindableProperty MaxLenghtProperty = BindableProperty.Create(
            nameof(MaxLenght), typeof(int), 
            typeof(ValidationEntryView), defaultValue: 50);

        public static readonly BindableProperty MinLenghtProperty = BindableProperty.Create(
             nameof(MinLenght), typeof(int), 
             typeof(ValidationEntryView), defaultValue: 5);

        public static readonly BindableProperty IsHiddenProperty = BindableProperty.Create(
             nameof(IsHidden), typeof(bool), typeof(ValidationEntryView));

        public static readonly BindableProperty CanInputProperty = BindableProperty.Create(
             nameof(CanInput), typeof(bool),
             typeof(ValidationEntryView), defaultValue: true);

        public static readonly BindableProperty IsReadonlyProperty = BindableProperty.Create(
             nameof(IsReadonly), typeof(bool),
             typeof(ValidationEntryView), defaultValue: false);

        public static readonly BindableProperty RegexProperty = BindableProperty.Create(
            nameof(Regex), typeof(string), 
            typeof(ValidationEntryView), defaultValue: string.Empty);

        public string TextValue { get => (string)GetValue(TextValueProperty); set => SetValue(TextValueProperty, value); }
        public bool IsHidden { get => (bool)GetValue(IsHiddenProperty); set => SetValue(IsHiddenProperty, value); }
        public bool CanInput { get => (bool)GetValue(CanInputProperty); set => SetValue(CanInputProperty, value); }
        public bool IsReadonly { get => (bool)GetValue(IsReadonlyProperty); set => SetValue(IsReadonlyProperty, value); }

        public int MaxLenght { get => (int)GetValue(MaxLenghtProperty); set => SetValue(MaxLenghtProperty, value); }
        public int MinLenght { get => (int)GetValue(MinLenghtProperty); set => SetValue(MinLenghtProperty, value); }
        public string Regex { get => (string)GetValue(RegexProperty); set => SetValue(RegexProperty, value); }
        public bool IsValidated
        {
            get => (bool)this.GetValue(IsValidatedProperty); set => this.SetValue(IsValidatedProperty, value);
        }
        public string DefaultText
        {
            get => (string)this.GetValue(DefaultTextProperty); set => this.SetValue(DefaultTextProperty, value);
        }
        public string ErrorText { get => (string)GetValue(ErrorTextProperty); set => SetValue(ErrorTextProperty, value); }
        public string LabelText
        {
            get => (string)this.GetValue(LabelTextProperty); set => this.SetValue(LabelTextProperty, value);
        }
        public ValidationEntryView() : base()
        {
            this.InitializeComponent();
            var validationBinding = new Binding("IsValidated", source: this, mode: BindingMode.OneWayToSource);
            var textValueBinding = new Binding("Text", source: this.TextField, mode: BindingMode.TwoWay);

            this.ValidationError.SetBinding(ValidationBehavior.IsValidProperty, validationBinding);
            this.ValidationError.IsValid = default!;
            
            this.SetBinding(ValidationEntryView.TextValueProperty, textValueBinding);
        }
        protected override void InvalidateLayout() => base.InvalidateLayout();
    }
#nullable disable
}

