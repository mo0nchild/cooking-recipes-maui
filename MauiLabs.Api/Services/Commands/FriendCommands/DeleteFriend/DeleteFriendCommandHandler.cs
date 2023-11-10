using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend
{
    public partial class DeleteFriendCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteFriendCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteFriendCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.FriendsList.Where(p => p.Id == request.RecordId).ExecuteDeleteAsync();
                if (requestResult < 0) throw new ApiServiceException("Запись о дружбе не найдена", requestType);
            }
        }
    }
}
