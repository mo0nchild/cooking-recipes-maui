using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.ApiModels.Commons
{
    public partial class ProblemDetails : object
    {
        public required string Title { get; set; } = default!;
        public required string Detail { get; set; } = default!;

        public required int Status { get; set; } = default!;
    }
}
