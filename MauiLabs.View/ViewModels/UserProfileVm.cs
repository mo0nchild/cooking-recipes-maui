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
        public UserProfileVm() : base()
        {
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
