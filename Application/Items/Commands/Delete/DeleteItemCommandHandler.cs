using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Item;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Items.Commands.Delete
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, DeleteItemResponse>
    {
        private readonly DatabaseContext _dbContext;

        public DeleteItemCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<DeleteItemResponse> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var shoppinglist = await _dbContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppinglistId, cancellationToken: cancellationToken);

            if (shoppinglist == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.ShoppinglistId));
            }

            var item = await _dbContext.Items
                .Where(x => x.Id == request.ItemId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (item == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorMissingItemId, request.ItemId));
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteItemResponse();
        }
    }
}
