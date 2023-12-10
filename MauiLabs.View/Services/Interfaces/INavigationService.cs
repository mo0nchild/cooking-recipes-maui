using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    using Parameters = IDictionary<string, object>;
    public interface INavigationService
    {
        public interface IQueryableNavigation { public void SetNavigationQuery(Parameters queries); }
        public Task NavigateToPage<TPage>(Shell shell, Parameters parameters = null) where TPage: Page;
    }
}
