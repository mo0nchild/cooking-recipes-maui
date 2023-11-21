using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Commons
{
    public partial class ViewServiceException : Exception
    {
        public ViewServiceException(string message) : base(message) { }
    }
}
