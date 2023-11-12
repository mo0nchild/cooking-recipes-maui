using MediatR;

namespace MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend
{
    public partial class DeleteRecommendCommand : IRequest
    {
        public required int RecordId { get; set; } = default!;
    }
}
