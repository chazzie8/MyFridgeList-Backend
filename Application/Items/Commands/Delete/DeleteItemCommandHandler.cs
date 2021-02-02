using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Item;

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
            var item = await _dbContext.Items
                .Where(x => x.Id == request.ItemId)
                .FirstOrDefaultAsync();

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();

            return new DeleteItemResponse();
        }
    }
}
