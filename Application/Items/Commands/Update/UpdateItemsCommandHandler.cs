using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Item;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Command.Update
{
    public sealed class UpdateItemsCommandHandler : IRequestHandler<UpdateItemsCommand, IEnumerable<ItemResponse>>
    {
        private readonly DatabaseContext _dbContext;

        public UpdateItemsCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<IEnumerable<ItemResponse>> Handle(UpdateItemsCommand request, CancellationToken cancellationToken)
        {
            var shoppinglist = await _dbContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppinglistId);

            if (shoppinglist == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
            }

            foreach (var item in shoppinglist.Items)
            {
                if(request.ItemIds.Contains(item.Id))
                {
                    item.Bought = true;
                }
                else
                {
                    item.Bought = false;
                }
            }

            await _dbContext.SaveChangesAsync();

            var response = await _dbContext.Items
                .Where(x => x.ShoppinglistId == shoppinglist.Id)
                .Select(x => new ItemResponse
                {
                    Id = x.Id,
                    Label = x.Label,
                    Bought = x.Bought,
                })
                .ToListAsync();

            return response;
        }
    }
}
