using MauiLabs.View.Pages;

namespace MauiLabs.View
{
    public partial class AppShell : Shell
    {
        public AppShell() : base()
        {
            this.InitializeComponent();

            Routing.RegisterRoute("main/userlist", typeof(UserListPage));
            Routing.RegisterRoute("maui/userlist/userprofile", typeof(UserProfilePage));
        }
    }
}