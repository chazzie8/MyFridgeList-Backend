using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Exceptions;
using MyFridgeListWebapi.Core.Models.Responses.Fridge;
using MyFridgeListWebapi.Properties;

namespace MyFridgeListWebapi.Application.Fridges.Commands.Edit
{
    public sealed class EditFridgeCommandHandler : IRequestHandler<EditFridgeCommand, EditFridgeResponse>
    {
        private readonly DatabaseContext _dbContext;

        public EditFridgeCommandHandler(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<EditFridgeResponse> Handle(EditFridgeCommand request, CancellationToken cancellationToken)
        {
            var fridge = await _dbContext.Fridges
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.FridgeId);

            if (fridge == null)
            {
                throw new NotFoundException(string.Format(Resources.ValidationErrorFridgeWithIdNotExists, request.FridgeId));
            }

            fridge.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return new EditFridgeResponse
            {
                Id = fridge.Id,
                Name = fridge.Name,
            };
        }
    }
}
