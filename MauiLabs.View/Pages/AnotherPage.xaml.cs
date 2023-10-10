using MauiLabs.Dal.Entities;
using MauiLabs.View.ViewModels;

namespace MauiLabs.View.Pages;

public partial class AnotherPage : ContentPage
{
	public AnotherPage(UserProfileVm userProfileVm) : base()
	{
		this.InitializeComponent();
		this.BindingContext = userProfileVm;
	}
}