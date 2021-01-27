using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Delete
{
    public sealed class DeleteFridgeCommandHandler : IRequestHandler<DeleteFridgeCommand, DeleteFridgeResponse>
    {
        private readonly DatabaseContext _dbContext;

        public DeleteFridgeCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<DeleteFridgeResponse> Handle(DeleteFridgeCommand request, CancellationToken cancellationToken)
        {
            var fridge = await _dbContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.FridgeId);

            _dbContext.Fridges.Remove(fridge);
            await _dbContext.SaveChangesAsync();

            return new DeleteFridgeResponse();
        }
    }
}
