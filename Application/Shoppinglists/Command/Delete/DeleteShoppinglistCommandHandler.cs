using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Delete
{
    public sealed class DeleteShoppinglistCommandHandler : IRequestHandler<DeleteShoppinglistCommand, DeleteShoppinglistResponse>
    {
        private readonly DatabaseContext _dbContext;

        public DeleteShoppinglistCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<DeleteShoppinglistResponse> Handle(DeleteShoppinglistCommand request, CancellationToken cancellationToken)
        {
            var shoppinglist = await _dbContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppinglistId, cancellationToken: cancellationToken);

            if (shoppinglist == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
            }

            _dbContext.Shoppinglists.Remove(shoppinglist);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteShoppinglistResponse();
        }
    }
}
