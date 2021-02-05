using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Shoppinglist;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Shoppinglists.Command.Edit
{
    public sealed class EditShoppinglistCommandHandler : IRequestHandler<EditShoppinglistCommand, EditShoppinglistResponse>
    {
        private readonly DatabaseContext _dbContext;

        public EditShoppinglistCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task<EditShoppinglistResponse> Handle(EditShoppinglistCommand request, CancellationToken cancellationToken)
        {
            var shoppinglist = await _dbContext.Shoppinglists
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppinglistId, cancellationToken: cancellationToken);

            if (shoppinglist == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorShoppinglistWithIdNotExists, request.ShoppinglistId));
            }

            shoppinglist.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new EditShoppinglistResponse
            {
                Id = shoppinglist.Id,
                Name = shoppinglist.Name,
            };
        }
    }
}
