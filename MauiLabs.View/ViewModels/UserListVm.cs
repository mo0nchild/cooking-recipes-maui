using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using MauiLabs.View.Pages;

namespace MauiLabs.View.ViewModels
{
    public partial class UserListVm : INotifyPropertyChanged
    {
        public ICommand AddProfile { get; set; } = default!;
        public ICommand SelectProfile { get; set; } = default!;

        public UserListVm() : base()
        {
            this.AddProfile = new Command(this.AddProfileHandler);
            this.SelectProfile = new Command(this.SelectProfileHandler);
            Task.Run(this.InitianlizeAsync).Wait();
        }

        protected virtual async Task InitianlizeAsync()
        {
            await Task.Run(() => { });
        }

        public virtual async void SelectProfileHandler(object @string)
        {
            await Application.Current.MainPage.DisplayAlert("Test", "Test" + @string, "Exit");
            // await Shell.Current.GoToAsync("./userprofile");
            
        }
        public virtual async void AddProfileHandler(object @string)
        {
            await Application.Current.MainPage.DisplayAlert("Test", "Test" + @string, "Exit");
        }

        public event PropertyChangedEventHandler PropertyChanged = default;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
