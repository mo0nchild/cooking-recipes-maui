using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientUnit
{
    public partial class AddIngredientUnitCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<AddIngredientUnitCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(AddIngredientUnitCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = this._mapper.Map<IngredientUnit>(request);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.IngredientUnits.FirstOrDefaultAsync(item => item.Name == mappedModel.Name) != null)
                {
                    throw new ApiServiceException("Данная единицы измерения уже создана", typeof(AddIngredientUnitCommand));
                }
                await dbcontext.IngredientUnits.AddRangeAsync(mappedModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
