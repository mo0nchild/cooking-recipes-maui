using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem
{
    public partial class AddIngredientItemCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<AddIngredientItemCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(AddIngredientItemCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = this._mapper.Map<IngredientItem>(request);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.IngredientItems.FirstOrDefaultAsync(item => item.Name == mappedModel.Name) != null)
                {
                    throw new ApiServiceException("Данный игредиент уже создан", typeof(AddIngredientItemCommand));
                }
                await dbcontext.IngredientItems.AddRangeAsync(mappedModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
