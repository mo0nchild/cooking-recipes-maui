using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
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

namespace MauiLabs.View.ViewModels
{
    public partial class UserProfileVm : INotifyPropertyChanged
    {
        private readonly IDbContextFactory<CookingRecipeDbContext> _factory;

        public ObservableCollection<UserProfile> UserProfiles { get; set; } = new();
        public ICommand AddProfile { get; set; } = default!;

        public UserProfileVm(IDbContextFactory<CookingRecipeDbContext> factory) : base()
        {
            using (var dbcontext = (this._factory = factory).CreateDbContext())
            {

                this.UserProfiles = new ObservableCollection<UserProfile>(dbcontext.UserProfiles.ToList());
            }
            this.AddProfile = new Command(this.AddProfileHandler);
        }

        public virtual void AddProfileHandler(object sender)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged = default;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
