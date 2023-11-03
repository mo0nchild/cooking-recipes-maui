using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.ViewModels
{
    public partial class UserProfileVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IDbContextFactory<CookingRecipeDbContext> _dbContextFactory;

        public UserProfile UserProfile { get; set; } = default!;

        public UserProfileVm(IDbContextFactory<CookingRecipeDbContext> factory) : base()
        {
            this._dbContextFactory = factory;
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
