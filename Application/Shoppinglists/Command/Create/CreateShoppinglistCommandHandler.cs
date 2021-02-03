using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Create
{
    public sealed class CreateShoppinglistCommandHandler : IRequestHandler<CreateShoppinglistCommand, CreateShoppinglistResponse>
    {
        private readonly DatabaseContext _dbContext;

        public CreateShoppinglistCommandHandler(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CreateShoppinglistResponse> Handle(CreateShoppinglistCommand request, CancellationToken cancellationToken)
        {
            var shoppinglist = new Shoppinglist
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
            };

            _dbContext.Shoppinglists.Add(shoppinglist);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateShoppinglistResponse
            {
                Id = shoppinglist.Id,
                Name = shoppinglist.Name,
            };
        }
    }
}
